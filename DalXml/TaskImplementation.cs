
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

/// <summary>
/// The implementation of task's CRUD methods in DAL
/// </summary>
internal class TaskImplementation : ITask
{
    const string taskFile = "tasks";
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    public int Create(Task item)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int id = Config.NextIdT;
        Task task = item with { ID = id };
        Tasks.Add(task);
        XMLTools.SaveListToXMLSerializer(Tasks, taskFile);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">The object's id to delete</param>
    /// <exception cref="DalDoesNotExistException">If the object not found</exception>
    public void Delete(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(taskFile);
        Task task = Tasks.FirstOrDefault(ele => ele.ID == id) ??
            throw new DalDoesNotExistException($"Task with ID={id} not exists");
        Tasks.Remove(task);
        task = task with { Complete = DateTime.Now };
        Tasks.Add(task);
        XMLTools.SaveListToXMLSerializer(Tasks, taskFile);
    }


    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">The object's id to read</param>
    /// <returns>The object or null if not found</returns>
    public Task? Read(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(taskFile);
        Task? task = Tasks.FirstOrDefault(ele => ele.ID == id);
        return task;
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(taskFile);
        return Tasks.FirstOrDefault(filter);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(taskFile);
        if (filter != null)
            return Tasks.Where(filter);
        return Tasks.Select(task => task);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="DalDoesNotExistException">If object not found</exception>
    public void Update(Task item)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(taskFile);
        Task task = Tasks.FirstOrDefault(ele => ele.ID == item.ID) ??
            throw new DalDoesNotExistException($"Task with ID={item.ID} not exists");
        Tasks.Remove(task);
        Tasks.Add(item);
        XMLTools.SaveListToXMLSerializer(Tasks, taskFile);
    }
}
