
using BlApi;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        if (boTask.alias == "")
           throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = replaceBoToDo(boTask);
        int id = _dal.Task.Create(doTask);
        if (boTask.dependList == null)
            return id;
        IEnumerable<int>  i = from BO.TaskInList task in boTask.dependList
             select _dal.Dependency.Create(new DO.Dependency(0, id, task.ID));
        return id;
    }


    public void Delete(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        IEnumerable<DO.Dependency?> dependencies = _dal.Dependency.ReadAll((d) => d.dependsOnTask == id);
        if (dependencies == null)
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalAlreadyExistException ex)
            {
                throw new BO.BlAlreadyExistException($"Task with ID={id} already exists", ex);
            }
        else throw new BO.BlDeletionImpossibleException($"Impossible to delete this task");
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        return replaceDoToBo(doTask);
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        if (filter != null)
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    let boTask = replaceDoToBo(doTask)
                    where filter(boTask)
                    select boTask);
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select replaceDoToBo(doTask));
    }

    public void Update(BO.Task boTask)
    {
        if (boTask.alias == "")
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = replaceBoToDo(boTask);
        try
        {
            _dal.Task.Delete(boTask.ID);
            int id = _dal.Task.Create(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.ID} does not exists", ex);
        }
    }

    DO.Task replaceBoToDo(BO.Task boTask)
    {
        bool milestone = boTask.milestone == null ? false : true;
        DO.EngineerExperiece? complexityLevel = null;
        if (boTask.complexityLevel != null)
            complexityLevel = (DO.EngineerExperiece)boTask.complexityLevel;
        return new DO.Task
         (boTask.ID,
          boTask.description,
          boTask.alias,
          milestone,
          boTask.requiredEffortTime,
          boTask.createdAt,
          boTask.baselineStart,
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

        IBl bl = BlApi.Factory.Get();
        BO.EngineerExperiece? complexityLevel = null;
        if (doTask.complexityLevel != null)
            complexityLevel = (BO.EngineerExperiece)doTask.complexityLevel;
        IEnumerable<BO.TaskInList?> taskInLists = dependList(doTask.ID);
        return new BO.Task()
        {
            ID = doTask.ID,
            description = doTask.description,
            alias = doTask.alias,
            dependList = taskInLists,
            milestone = null,//calcMilestone(doTask.ID)
            requiredEffortTime = doTask.requiredEffortTime,
            status = calcStatus(doTask),
            createdAt = doTask.createdAt,
            baselineStart = doTask.baselineStart,
            start = doTask.start,
            forecastDate = doTask.forecastDate,
            deadline = doTask.deadline,
            complete = doTask.complete,
            deliverable = doTask.deliverable,
            remarks = doTask.remarks,
            engineer = bl.EngineerInTask.Read(doTask.engineerId),
            complexityLevel = complexityLevel
        };
    }

    IEnumerable<BO.TaskInList?> dependList(int ID)
    {
        IBl bl = BlApi.Factory.Get();
        return _dal.Dependency.ReadAll(depend => depend.dependentTask == ID)
            .Select(depend => depend == null ? null : bl.TaskInList.Read(depend.dependsOnTask));
    }

    //IEnumerable<BO.MilestoneInList?> calcMilestone(IEnumerable<BO.TaskInList?> dependTask)
    //{
    //    IBl bl = BlApi.Factory.Get();
    //    return dependTask.Where(task => task != null && _dal.Task.Read(task.ID) != null 
    //                                    && (_dal.Task.Read(task.ID)!.milestone == true))
    //        .Select(task=>task==null?null:bl.MilestoneInList.Read(task.ID));

    //}

    //BO.MilestoneInTask? calcMilestone(int ID)
    //{
    //    IBl bl = BlApi.Factory.Get();
    //    DO.Task? doMilestone = _dal.Task.ReadAll(task => task.milestone == true)
    //           .FirstOrDefault(task => task != null && _dal.Dependency.
    //                         Read(depend => depend.dependentTask == task.ID && depend.dependsOnTask == ID) != null);
    //    if(doMilestone == null)
    //        return null;
    //    return bl.MilestoneInTask.Read(doMilestone.ID);
    //}

    BO.Status calcStatus(DO.Task doTask)
    {
        DateTime now= DateTime.Now;
        if (doTask.complete < now) return (BO.Status)4;
        if (doTask.deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.forecastDate < now && doTask.deadline > now) return (BO.Status)3;
        if(doTask.start<now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }


}
