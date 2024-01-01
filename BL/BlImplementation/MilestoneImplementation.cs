using BlApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private IBl bl = Factory.Get();
    public BO.Milestone? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Milestone with ID={id} does not exist");
        return replaceTaskToMilestone(doTask);
    }

    public BO.Milestone? Read(Func<BO.Milestone,bool> filter)
    {
       return bl.Milestone.ReadAll().FirstOrDefault(filter);
    }
    public IEnumerable<BO.Milestone> ReadAll()
    {
        return _dal.Task.ReadAll().Where(task => task != null && task.milestone).Select(task => replaceTaskToMilestone(task!));
    }

    public void Update(BO.Milestone milestone)
    {
        if (milestone.alias == "")
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = _dal.Task.Read(milestone.ID) ??
                        throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists");
        try
        {
            _dal.Task.Delete(milestone.ID);
            DO.Task updatedTask = doTask
                with
            { alias = milestone.alias, description = milestone.description, remarks = milestone.remarks };
            int id = _dal.Task.Create(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists", ex);
        }
    }


    BO.Milestone replaceTaskToMilestone(DO.Task task)
    {
        IEnumerable<BO.TaskInList?> taskInLists = dependList(task.ID);
        return new BO.Milestone()
        {
            ID = task.ID,
            description = task.description,
            alias = task.alias,
            status = calcStatus(task),
            createdAt = task.createdAt,
            start = task.start,
            forecastEndDate = task.forecastEndDate,
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
        if (doTask.forecastEndDate < now && doTask.deadline > now) return (BO.Status)3;
        if (doTask.start < now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }
    IEnumerable<BO.TaskInList?> dependList(int ID)
    {
        return _dal.Dependency.ReadAll(depend => depend.dependentTask == ID)
            .Select(depend => depend == null ? null : bl.TaskInList.Read(depend.dependsOnTask));
    }
    int? calcCompletionPercentage(IEnumerable<BO.TaskInList?> tasksInList)
    {
        int tasksAmount = tasksInList.Count();
        int doneTasks = tasksInList.Count(t => t != null && t.status == (BO.Status)4);
        return (100 * doneTasks) / tasksAmount;
    }

    public void CreateSchedual(DateTime startProject, DateTime endProject)
    {
        CreateMilestones();
        calcTimes(startProject,endProject);

    }

    void CreateMilestones()
    {
        int nextId = 1;
        _dal.Task.ReadAll(task => _dal.Dependency.ReadAll(depend => depend.dependentTask == task.ID) == null)
            .Where(task => task != null).Select(task => CreateStartMilestone(task!.ID));
        IEnumerable <(int? key, IEnumerable<DO.Dependency> dependencies)> list =
            from dependency in _dal.Dependency.ReadAll()
            group dependency by dependency.dependentTask into dependGroup
            orderby dependGroup.Key
            select (dependGroup.Key, dependGroup.Select(depend=>depend));
        IEnumerable<int> milestoneIDs = list
            .Select(task => CreateMilestone(task.key, task.dependencies, nextId++));
        _dal.Task.ReadAll(task => _dal.Dependency.ReadAll(depend => depend.dependsOnTask == task.ID) == null)
            .Where(task => task != null).Select(task => CreateEndMilestone(task!.ID));
    }

    int CreateMilestone(int? key, IEnumerable<DO.Dependency> dependencies,int nextId)
    {
        int? id;
        id = (from milestone in bl.Milestone.ReadAll()
              where  milestone.dependencies.Any(depend1 => depend1!=null?dependencies.Any(depend2 => depend2.dependsOnTask == depend1.ID):false)
              select milestone.ID).FirstOrDefault();
        if (id == null)
        {
            id = _dal.Task.Create(new DO.Task(0, $"Milestone number: {nextId}", $"M{nextId}", true,
                TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, "", "", null, null));
        }
            IEnumerable<int> dependIDs = from DO.Dependency dependTask in dependencies
                                         select _dal.Dependency.Create(dependTask with { dependentTask = id });
        var i = from DO.Dependency dependTask in dependencies
                where dependTask.dependentTask == key
                select DeleteDependency(dependTask.ID);
        _dal.Dependency.Create(new DO.Dependency(0, key, id));
        return (int)id;
    }

    int CreateStartMilestone(int taskID)
    {
        if (bl.Milestone.Read(milestone => milestone.alias == "START") == null)
            _dal.Task.Create(new DO.Task(0, "Start milestone", "START", true,
               TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
               DateTime.MinValue, "", "", null, null));
        _dal.Dependency.Create(new DO.Dependency(0, taskID, 0));
        return 0;
    }

    int CreateEndMilestone(int taskID)
    {
        int id;
        BO.Milestone? endMilestone = bl.Milestone.Read(milestone => milestone.alias == "END");
        if (endMilestone == null)
            id = _dal.Task.Create(new DO.Task(0, "End milestone", "END", true,
                TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, "", "", null, null));
        else
            id = endMilestone.ID;
        _dal.Dependency.Create(new DO.Dependency(0,id , taskID));
        return id; 
    }

    int DeleteDependency( int id)
    {
        _dal.Dependency.Delete(id);
        return id;
    }

    void calcTimes(DateTime startProject, DateTime endProject)
    {
       BO.Milestone firstMilestone = bl.Milestone.ReadAll().First();
        bl.Milestone.Update(new BO.Milestone() { 
            ID=firstMilestone.ID,
            alias = firstMilestone.alias,
            description = firstMilestone.description,
            status = firstMilestone.status,
            createdAt = firstMilestone.createdAt,
            start=firstMilestone.start,
            forecastEndDate=firstMilestone.forecastEndDate,
            deadline= startProject,
            complete=firstMilestone.complete,
            completionPercentage=firstMilestone.completionPercentage,
            remarks=firstMilestone.remarks,
            dependencies=firstMilestone.dependencies
        });
        IEnumerable<BO.Milestone> milestonses = bl.Milestone.ReadAll();
        var i =from milestone in milestonses
               where milestone.alias!="START"
               let 
    }

    DateTime max()

    public void CreateSchedual()
    {
        throw new NotImplementedException();
    }
}
