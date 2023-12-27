using BlApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.Milestone? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Milestone with ID={id} does not exist");
        return replaceTaskToMilestone(doTask);
    }

    public void Update(BO.Milestone milestone)
    {
        if (milestone.alias == "")
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = _dal.Task.Read(milestone.ID)??
                        throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists");
        try
        {
            _dal.Task.Delete(milestone.ID);
            DO.Task updatedTask = doTask
                with { alias = milestone.alias, description = milestone.description, remarks = milestone.remarks };
            int id = _dal.Task.Create(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists", ex);
        }
    }


    BO.Milestone replaceTaskToMilestone(DO.Task task) {
        IBl bl = Factory.Get();
        IEnumerable<BO.TaskInList?> taskInLists = dependList(task.ID);
        return new BO.Milestone()
        {
            ID = task.ID,
            description = task.description,
            alias = task.alias,
            status = calcStatus(task),
            createdAt = task.createdAt,
            start = task.start,
            forecastDate = task.forecastDate,
            deadline = task.deadline,
            complete = task.complete,
            remarks = task.remarks,
            completionPercentage = calcCompletionPercentage(taskInLists),
            dependencies = taskInLists
        };
    }
    BO.Status calcStatus(DO.Task doTask)
    {
        DateTime now = DateTime.Now;
        if (doTask.complete < now) return (BO.Status)4;
        if (doTask.deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.forecastDate < now && doTask.deadline > now) return (BO.Status)3;
        if (doTask.start < now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }
    IEnumerable<BO.TaskInList?> dependList(int ID)
    {
        IBl bl = Factory.Get();
        return _dal.Dependency.ReadAll(depend => depend.dependentTask == ID)
            .Select(depend => depend == null ? null : bl.TaskInList.Read(depend.dependsOnTask));
    }
    int? calcCompletionPercentage(IEnumerable<BO.TaskInList?> tasksInList)
    {
        int tasksAmount = tasksInList.Count();
        int doneTasks = tasksInList.Count(t => t != null && t.status == (BO.Status)4);
        return (100*doneTasks)/tasksAmount;
    }

    public void CreateSchedual()
    {
        throw new NotImplementedException();
    }
}
