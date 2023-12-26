
using BlApi;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        if (boTask.alias == "" || boTask.ID > 0)
            // לשאול האם צריך לבדוק תקינות מזהה
            // כי הוא מספר רץ
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = replaceBoToDo(boTask);
        int id = _dal.Task.Create(doTask);
        if (boTask.dependList == null)
            return id;
        IEnumerable<int>  i = (from BO.TaskInList task in boTask.dependList
             select _dal.Dependency.Create(new DO.Dependency(0, id, task.ID)));
        return id;
    }


    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();

    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    DO.Task replaceBoToDo(BO.Task boTask)
    {
        bool milestone = boTask.milestone == null ? false : true;
        DO.EngineerExperiece? complexityLevel = null;
        if (boTask.complexityLevel != null)
            complexityLevel = (DO.EngineerExperiece)boTask.complexityLevel;
        return new DO.Task
         (boTask.ID,
          boTask.desciption,
          boTask.alias,
          milestone,
          boTask.createdAt,
          boTask.start,
          boTask.forecastDate,
          boTask.deadline,
          boTask.complete,
          boTask.deliverable,
          boTask.remarks,
          boTask.engineer?.ID,
          complexityLevel);
    }
    BO.Task replaceDoToBo(DO.Task doTask)
    {
        BO.EngineerExperiece? complexityLevel = null;
        if (doTask.complexityLevel != null)
            complexityLevel = (BO.EngineerExperiece)doTask.complexityLevel;
        IEnumerable<BO.TaskInList?> taskInLists = dependList(doTask.ID);
        return new BO.Task()
        {
            ID = doTask.ID,
            desciption = doTask.desciption,
            alias = doTask.alias,
            dependList=taskInLists,
            milestone=calcMilestone(taskInLists),
            createdAt = doTask.createdAt,
            start = doTask.start,
            forecastDate = doTask.forecastDate,
            deadline = doTask.deadline,
            complete = doTask.complete,
            deliverable = doTask.deliverable,
            remarks = doTask.remarks,
            engineer = doTask.engineer.ID,
            complexityLevel = complexityLevel
        };
    }

    IEnumerable<BO.TaskInList?> dependList(int ID)
    {
        IBl bl = BlApi.Factory.Get();
        return _dal.Dependency.ReadAll(depend => depend.dependentTask == ID)
            .Select(depend => depend == null ? null : bl.TaskInList.Read(depend.dependsOnTask));
    }

    IEnumerable<BO.MilestoneInList?> calcMilestone(IEnumerable<BO.TaskInList?> dependTask)
    {
        IBl bl = BlApi.Factory.Get();
        return dependTask.Where(task => task != null && _dal.Task.Read(task.ID) != null 
                                        && (_dal.Task.Read(task.ID)!.milestone == true))
            .Select(task=>task==null?null:bl.MilestoneInList.Read(task.ID));
    }
}
