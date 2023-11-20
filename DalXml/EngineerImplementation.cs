
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    const string engineerFile = "engineers";

    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
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
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="Exception">if the object not found</exception>
    public void Delete(int id)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        Engineer engineer = Engineers.FirstOrDefault(ele => ele.ID == id) ??
            throw new DalDoesNotExistExeption($"Engineer with ID={id} not exists");
        Engineers.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(Engineers, engineerFile);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
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
    /// Reads all entity objects
    /// </summary>
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
    /// <exception cref="Exception">if object not found</exception>
    /// 
    public void Update(Engineer item)
    {
        List<Engineer> Engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(engineerFile);
        Engineer engineer = Engineers.FirstOrDefault(ele => ele.ID == item.ID) ??
           throw new DalDoesNotExistExeption($"Engineer with ID={item.ID} not exists");
        Engineers.Remove(engineer);
        Engineers.Add(item);
        XMLTools.SaveListToXMLSerializer(Engineers, engineerFile);
    }
}
