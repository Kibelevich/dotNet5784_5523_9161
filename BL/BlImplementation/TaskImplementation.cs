
using BlApi;
using DalApi;

namespace BlImplementation;

/// <summary>
/// The implementation of task's CRUD methods in BL
/// </summary>

internal class TaskImplementation : BlApi.ITask
{
    private IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="boTask">the object to add</param>
    /// <returns>the object's id</returns>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
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

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    /// <exception cref="BO.BlDeletionImpossibleException">if it's impossible to delete the entity</exception>
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
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={id} already exists", ex);
            }
        else throw new BO.BlDeletionImpossibleException($"Impossible to delete this task");
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does not exist");
        return replaceDoToBo(doTask);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Collection of all objects</returns>
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

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="boTask">The object to update</param>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlDoesNotExistException">if object not found</exception>
    public void Update(BO.Task boTask)
    {
        if (boTask.alias == "" || boTask.engineer != null &&
            (_dal.Engineer.Read(boTask.engineer.ID) == null
                || _dal.Engineer.Read(boTask.engineer.ID)!.name != boTask.engineer.name))
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Task doTask = replaceBoToDo(boTask);
        try
        {
            //_dal.Task.Delete(boTask.ID);
            //int id = _dal.Task.Create(doTask);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.ID} does not exists", ex);
        }
    }

    /// <summary>
    /// Replace the entity from BL object to DAL object
    /// </summary>
    /// <param name="boTask">the BL object to replace</param>
    /// <returns>the DAL object</returns>
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
          boTask.forecastEndDate,
          boTask.deadline,
          boTask.complete,
          boTask.deliverable,
          boTask.remarks,
          boTask.engineer?.ID,
          complexityLevel);
    }

    /// <summary>
    /// Replace the entity from DAL object to BL object
    /// </summary>
    /// <param name="doTask">the DAL object to replace</param>
    /// <returns>the BL object</returns>
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
            forecastEndDate = doTask.forecastEndDate,
            deadline = doTask.deadline,
            complete = doTask.complete,
            deliverable = doTask.deliverable,
            remarks = doTask.remarks,
            engineer = bl.EngineerInTask.Read(doTask.engineerId),
            complexityLevel = complexityLevel
        };
    }


    /// <summary>
    /// Finds all tasks that depend on the current task 
    /// </summary>
    /// <param name="ID">The id of the current task</param>
    /// <returns>The list of pending tasks</returns>
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


    /// <summary>
    /// Calculates the task's status according to the dates
    /// </summary>
    /// <param name="doTask">The task to calculate for</param>
    /// <returns>The task's status </returns>
    /// <exception cref="BO.BlDeadlinePassedException">If the dead line passed</exception>
    BO.Status calcStatus(DO.Task doTask)
    {
        DateTime now= DateTime.Now;
        if (doTask.complete < now) return (BO.Status)4;
        if (doTask.deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.forecastEndDate < now && doTask.deadline > now) return (BO.Status)3;
        if(doTask.start<now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }


}
