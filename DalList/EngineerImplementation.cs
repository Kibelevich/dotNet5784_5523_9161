
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The implementation of engineer's CRUD methods in DAL
/// </summary>

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    /// <exception cref="DalAlreadyExistException">if the object alredy exists</exception>
    public int Create(Engineer item)
    {
        if(DataSource.Engineers.Any(engineer => engineer.ID == item.ID))
            throw new DalAlreadyExistException($"Engineer with ID={item.ID} already exists");
        DataSource.Engineers.Add(item);
        return item.ID;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="DalDoesNotExistException">if the object not found</exception>
    public void Delete(int id)
    {
        Engineer engineer = DataSource.Engineers.FirstOrDefault(ele => ele.ID == id)??
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        DataSource.Engineers.Remove(engineer);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public Engineer? Read(int id)
    {
        Engineer? eng = DataSource.Engineers.FirstOrDefault(ele => ele.ID == id);
        return eng;
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.First(filter);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Collection of all objects</returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if(filter != null)
            return DataSource.Engineers.Where(filter);
        return DataSource.Engineers.Select(engineer => engineer);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="DalDoesNotExistException">if object not found</exception>
    public void Update(Engineer item)
    {
        Engineer engineer = DataSource.Engineers.FirstOrDefault(ele => ele.ID == item.ID) ??
           throw new DalDoesNotExistException($"Engineer with ID={item.ID} not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}
