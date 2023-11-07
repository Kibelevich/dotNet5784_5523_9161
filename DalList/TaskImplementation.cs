
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class TaskImplementation : ITask
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
        Task task = DataSource.Tasks.Find(ele => ele.ID == id) ??
            throw new Exception($"Task with ID={id} not exists");
        DataSource.Tasks.Remove(task);
        task = task with { complete = DateTime.Now };
        DataSource.Tasks.Add(task);
    }


    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
    /// <exception cref="Exception">if the object not found</exception>
    public Task? Read(int id)
    {
        Task? task = null;
        DataSource.Tasks.ForEach(element =>
        {
            if (element.ID == id)
                task = element;
        });
        return task;
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Task item)
    {
        Task task = DataSource.Tasks.Find(ele => ele.ID == item.ID) ??
            throw new Exception($"Task with ID={item.ID} not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(item);
    }
}
