
using BlApi;
using DalApi;
using System.Text.RegularExpressions;

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
        if (boTask.Alias == "" || boTask.Start < boTask.CreatedAt || boTask.BaselineStart < boTask.CreatedAt
            || boTask.BaselineStart > boTask.ForecastEndDate || boTask.Start > boTask.ForecastEndDate ||
            boTask.Deadline < boTask.ForecastEndDate || boTask.Deadline < boTask.Complete)
            throw new BO.BlIllegalPropertyException($"Illegal property");
            DO.Task doTask = ReplaceBoToDo(boTask);
        int id = _dal.Task.Create(doTask with { CreatedAt = DateTime.Now });
        if (boTask.DependList == null)
            return id;
        IEnumerable<int>  i = from BO.TaskInList task in boTask.DependList
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
        return ReplaceDoToBo(doTask);
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
                    let boTask = ReplaceDoToBo(doTask)
                    where filter(boTask)
                    select boTask);
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select ReplaceDoToBo(doTask));
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="boTask">The object to update</param>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlDoesNotExistException">if object not found</exception>
    public void Update(BO.Task boTask)
    {
        if (boTask.Alias == "" || boTask.Start < boTask.CreatedAt || boTask.BaselineStart < boTask.CreatedAt
             || boTask.BaselineStart > boTask.ForecastEndDate || boTask.Start > boTask.ForecastEndDate ||
             boTask.Deadline < boTask.ForecastEndDate || boTask.Deadline < boTask.Complete)
            throw new BO.BlIllegalPropertyException($"Illegal property");
        if (boTask.Engineer != null)
        {
            DO.Engineer engineer = _dal.Engineer.Read(boTask.Engineer.ID) ??
                throw new BO.BlIllegalPropertyException("Illegal property");
            if (engineer.Name != boTask.Engineer.Name
                ||(int)boTask.ComplexityLevel < (int)engineer.Level)
                throw new BO.BlIllegalPropertyException("Illegal property");
        }
            DO.Task doTask = ReplaceBoToDo(boTask);
        try
        {
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
    DO.Task ReplaceBoToDo(BO.Task boTask)
    {
        bool milestone = boTask.Milestone == null ? false : true;
        return new DO.Task
         (boTask.ID,
          boTask.Description,
          boTask.Alias,
          milestone,
          boTask.RequiredEffortTime,
          boTask.CreatedAt,
          boTask.BaselineStart,
          boTask.Start,
          boTask.ForecastEndDate,
          boTask.Deadline,
          boTask.Complete,
          boTask.Deliverable,
          boTask.Remarks,
          boTask.Engineer?.ID,
          (DO.EngineerExperiece)boTask.ComplexityLevel!);
    }

    /// <summary>
    /// Replace the entity from DAL object to BL object
    /// </summary>
    /// <param name="doTask">the DAL object to replace</param>
    /// <returns>the BL object</returns>
    BO.Task ReplaceDoToBo(DO.Task doTask)
    {

        IBl bl = BlApi.Factory.Get();
        IEnumerable<BO.TaskInList?> taskInLists = DependList(doTask.ID);
        return new BO.Task()
        {
            ID = doTask.ID,
            Description = doTask.Description,
            Alias = doTask.Alias,
            DependList = taskInLists,
            Milestone = null,//calcMilestone(doTask.ID)
            RequiredEffortTime = doTask.RequiredEffortTime,
            Status = CalcStatus(doTask),
            CreatedAt = doTask.CreatedAt,
            BaselineStart = doTask.BaselineStart,
            Start = doTask.Start,
            ForecastEndDate = doTask.ForecastEndDate,
            Deadline = doTask.Deadline,
            Complete = doTask.Complete,
            Deliverable = doTask.Deliverable,
            Remarks = doTask.Remarks,
            Engineer = doTask.EngineerId == 0 ? null : bl.EngineerInTask.Read(doTask.EngineerId),
            ComplexityLevel = (BO.EngineerExperiece)doTask.ComplexityLevel
        };
    }


    /// <summary>
    /// Finds all tasks that depend on the current task 
    /// </summary>
    /// <param name="ID">The id of the current task</param>
    /// <returns>The list of pending tasks</returns>
    IEnumerable<BO.TaskInList?> DependList(int ID)
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
    BO.Status CalcStatus(DO.Task doTask)
    {
        DateTime now= DateTime.Now;
        if (doTask.Complete < now) return (BO.Status)4;
        if (doTask.Deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.ForecastEndDate < now && doTask.Deadline > now) return (BO.Status)3;
        if(doTask.Start<now) return (BO.Status)2;
        if (doTask.Deadline < now) return (BO.Status)1;
        return 0;
    }


}
