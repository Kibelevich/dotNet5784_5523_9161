using System.Text.RegularExpressions;
using BlApi;

namespace BlImplementation;


/// <summary>
/// The implementation of engineer's CRUD methods in BL
/// </summary>

internal class EngineerImplementation :IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="boEngineer">the object to add</param>
    /// <returns>the object's id</returns>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlAlreadyExistException">if the entity alredy exists</exception>
    public int Create(BO.Engineer boEngineer)
    {

        if (boEngineer.Name == "" || boEngineer.Email == "" || boEngineer.Level == 0 )
            throw new BO.BlIllegalPropertyException("Missing data");
        if (boEngineer.ID < 200000000 || boEngineer.ID > 400000000 || boEngineer.Cost <= 0 || !Regex.IsMatch(boEngineer.Email, @"(@)(.+)$"))
            throw new BO.BlIllegalPropertyException("Invalid property");
        if ( boEngineer.CurrentTask!.ID != 0)
        { 
            DO.Task currentTask = _dal.Task.Read(boEngineer.CurrentTask.ID) ??
                throw new BO.BlIllegalPropertyException("The task did not found");
            if (currentTask.Alias != boEngineer.CurrentTask.Alias)
                throw new BO.BlIllegalPropertyException("The task did not found");
            if ((int)boEngineer.Level < (int)currentTask.ComplexityLevel)
                throw new BO.BlIllegalPropertyException("The task is not suitable for this engineer");
            try
            {
                // Update the ID of an engineer that belongs to the current task
                DO.Task taskToUpdate = currentTask with { EngineerId = boEngineer.ID };
                _dal.Task.Update(taskToUpdate);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.CurrentTask.ID} does not exists", ex);
            }
        }
        DO.Engineer doEngineer = ReplaceBoToDo(boEngineer);
        try
        {
            return _dal.Engineer.Create(doEngineer);
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={boEngineer.ID} already exists", ex);
        }
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    /// <exception cref="BO.BlDeletionImpossibleException">if it's impossible to delete the entity</exception>
    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
        IEnumerable<DO.Task?> tasks = _dal.Task.ReadAll((t) => t.EngineerId == id);
        if (tasks == null)
            try
            {
                _dal.Engineer.Delete(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} already exists", ex);
            }
        else throw new BO.BlDeletionImpossibleException($"Impossible to delete this engineer");
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
        return ReplaceDoToBo(doEngineer);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Collection of all objects</returns>
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                   let boEngineer = ReplaceDoToBo(doEngineer)
                   where filter(boEngineer)
                   select boEngineer;
        }
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
               select ReplaceDoToBo(doEngineer);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="boEngineer">The object to update</param>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlDoesNotExistException">if object not found</exception>
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.Name == "" || boEngineer.Email == "" || boEngineer.Level == 0)
            throw new BO.BlIllegalPropertyException("Missing data");
        if (boEngineer.ID < 200000000 || boEngineer.ID > 400000000 || boEngineer.Cost <= 0 || !Regex.IsMatch(boEngineer.Email, @"(@)(.+)$"))
            throw new BO.BlIllegalPropertyException("Invalid property");
        if ( boEngineer.CurrentTask!.ID != 0)
        {
            DO.Task currentTask = _dal.Task.Read(boEngineer.CurrentTask.ID) ??
                throw new BO.BlIllegalPropertyException("The task did not found");
            if (currentTask.Alias != boEngineer.CurrentTask.Alias)
                throw new BO.BlIllegalPropertyException("The task did not found");
            if ((int)boEngineer.Level < (int)currentTask.ComplexityLevel)
                throw new BO.BlIllegalPropertyException("The task is not suitable for this engineer");
            try
            {
                // Update the ID of an engineer that belongs to the current task
                DO.Task taskToUpdate = currentTask with { EngineerId = boEngineer.ID };
                _dal.Task.Update(taskToUpdate);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.CurrentTask.ID} does not exists", ex);
            }
        }
        DO.Engineer doEngineer = ReplaceBoToDo(boEngineer);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.ID} does not exists", ex);
        }
    }

    /// <summary>
    /// Replace the entity from DAL object to BL object
    /// </summary>
    /// <param name="doEngineer">the DAL object to replace</param>
    /// <returns>the BL object</returns>
    BO.Engineer ReplaceDoToBo(DO.Engineer doEngineer)
    {
        DO.Task? doCTask = _dal.Task.Read(task => task.EngineerId == doEngineer.ID);
        BO.TaskInEngineer? boCTask = new BO.TaskInEngineer() { ID = 0, Alias = "" };
        if (doCTask != null)
            boCTask = new BO.TaskInEngineer() { ID = doCTask.ID, Alias = doCTask.Alias };
        return new BO.Engineer()
        {
            ID = doEngineer.ID,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperiece)doEngineer.Level,
            Cost = doEngineer.Cost,
            CurrentTask = boCTask
        };
    }

    /// <summary>
    /// Replace the entity from BL object to DAL object
    /// </summary>
    /// <param name="boEngineer">the BL object to replace</param>
    /// <returns>the DAL object</returns>
    DO.Engineer ReplaceBoToDo(BO.Engineer boEngineer)
    {
        return new DO.Engineer
        (boEngineer.ID, boEngineer.Name, boEngineer.Email, (DO.EngineerExperiece)boEngineer.Level, boEngineer.Cost);
    }
}

