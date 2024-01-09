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
        if (boEngineer.ID <= 0 || boEngineer.name == "" || boEngineer.cost < 0 || !Regex.IsMatch(boEngineer.email, @"(@)(.+)$")
            || boEngineer.currentTask != null && (_dal.Task.Read(boEngineer.currentTask.ID) == null
            || _dal.Task.Read(boEngineer.currentTask!.ID)!.alias != boEngineer.currentTask.alias))
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = replaceBoToDo(boEngineer);
        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
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
        IEnumerable<DO.Task?> tasks = _dal.Task.ReadAll((t) => t.engineerId == id);
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
    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
        return replaceDoToBo(doEngineer);
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
                   let boEngineer = replaceDoToBo(doEngineer)
                   where filter(boEngineer)
                   select boEngineer;
        }
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
               select replaceDoToBo(doEngineer);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="boEngineer">The object to update</param>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlDoesNotExistException">if object not found</exception>
    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || boEngineer.name == "" || boEngineer.cost < 0 || !Regex.IsMatch(boEngineer.email, @"(@)(.+)$"))
            throw new BO.BlIllegalPropertyException($"Illegal property");
        if(boEngineer.currentTask != null)
        {
            if (_dal.Task.Read(boEngineer.currentTask.ID) == null
                || _dal.Task.Read(boEngineer.currentTask.ID)!.alias != boEngineer.currentTask.alias)
                    throw new BO.BlIllegalPropertyException($"Illegal property");
            try
            {
                DO.Task taskToUpdate = _dal.Task.Read(boEngineer.currentTask.ID)! with { engineerId = boEngineer.ID };
                _dal.Task.Update(taskToUpdate);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Task with ID={boEngineer.currentTask.ID} does not exists", ex);
            }

        }
        DO.Engineer doEngineer = replaceBoToDo(boEngineer);
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
    BO.Engineer replaceDoToBo(DO.Engineer doEngineer)
    {
        DO.Task? doCTask = _dal.Task.Read(task => task.engineerId == doEngineer.ID);
        BO.TaskInEngineer? boCTask = null;
        if (doCTask != null)
            boCTask = new BO.TaskInEngineer() { ID = doCTask.ID, alias = doCTask.alias };
        return new BO.Engineer()
        {
            ID = doEngineer.ID,
            name = doEngineer.name,
            email = doEngineer.email,
            level = (BO.EngineerExperiece)doEngineer.level,
            cost = doEngineer.cost,
            currentTask = boCTask
        };
    }
    /// <summary>
    /// Replace the entity from BL object to DAL object
    /// </summary>
    /// <param name="boEngineer">the BL object to replace</param>
    /// <returns>the DAL object</returns>
    DO.Engineer replaceBoToDo(BO.Engineer boEngineer)
    {
        return new DO.Engineer
        (boEngineer.ID, boEngineer.name, boEngineer.email, (DO.EngineerExperiece)boEngineer.level, boEngineer.cost);
    }

}

