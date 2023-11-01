
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

internal class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
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
    /// <exception cref="Exception">if the object not found</exception>
    public void Delete(int id)
    {
        Engineer engineer = DataSource.Engineers.FirstOrDefault(ele => ele.ID == id)??
            throw new DalDoesNotExistExeption($"Engineer with ID={id} not exists");
        DataSource.Engineers.Remove(engineer);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
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
    /// <returns>List of all objects</returns>
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
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Engineer item)
    {
        Engineer engineer = DataSource.Engineers.FirstOrDefault(ele => ele.ID == item.ID) ??
           throw new DalDoesNotExistExeption($"Engineer with ID={item.ID} not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}
