
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The implementation of task's CRUD methods in DAL
/// </summary>
internal class TaskImplementation : ITask
{
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    public int Create(Task item)
    {
        int id = DataSource.Config.NextIdT;
        Task task = item with { ID = id };
        DataSource.Tasks.Add(task);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="Exception">if the object not found</exception>
    public void Delete(int id)
    {
        Task task = DataSource.Tasks.FirstOrDefault(ele => ele.ID == id)??
            throw new DalDoesNotExistException($"Task with ID={id} not exists");
        DataSource.Tasks.Remove(task);
        task = task with { Complete = DateTime.Now };
        DataSource.Tasks.Add(task);
    }


    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.FirstOrDefault(ele => ele.ID == id);
        return task;
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.First(filter);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.Tasks.Where(filter);
        return DataSource.Tasks.Select(task => task);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Task item)
    {
        Task task = DataSource.Tasks.FirstOrDefault(ele => ele.ID == item.ID)??
            throw new DalDoesNotExistException($"Task with ID={item.ID} not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(item);
    }
}
