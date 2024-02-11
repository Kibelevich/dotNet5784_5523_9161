
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

/// <summary>
/// The implementation of engineer's CRUD methods in DAL
/// </summary>
internal class EngineerImplementation : IEngineer
{
    const string engineerFile = "engineers";

    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">The object to add</param>
    /// <returns>The object's id</returns>
    /// <exception cref="DalAlreadyExistException">If the object already exists</exception>
    public int Create(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        if (Engineers.Any(engineer => engineer.ID == item.ID))
            throw new DalAlreadyExistException($"Engineer with ID={item.ID} already exists");
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, engineerFile);
        return item.ID;
    }


    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">The object's id to delete</param>
    /// <exception cref="DalDoesNotExistException">If the object not found</exception>
    public void Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        Engineer engineer = Engineers.FirstOrDefault(ele => ele.ID == id) ??
            throw new DalDoesNotExistException($"Engineer with ID={id} not exists");
        Engineers.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(Engineers, engineerFile);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">The object's id to read</param>
    /// <returns>The object or null if not found</returns>
    public Engineer? Read(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        Engineer? eng = Engineers.FirstOrDefault(ele => ele.ID == id);
        return eng;
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        return Engineers.First(filter);
    }

    /// <summary>
    ///  Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>List of all objects</returns>
    public IEnumerable<Engineer> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        if (filter != null)
            return Engineers.Where(filter);
        return Engineers.Select(engineer => engineer);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="DalDoesNotExistException">If object not found</exception>
    /// 
    public void Update(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        Engineer engineer = Engineers.FirstOrDefault(ele => ele.ID == item.ID) ??
           throw new DalDoesNotExistException($"Engineer with ID={item.ID} not exists");
        Engineers.Remove(engineer);
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, engineerFile);
    }
}
