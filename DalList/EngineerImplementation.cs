
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    public int Create(Engineer item)
    {
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == item.ID)
                throw new Exception($"Engineer with ID={item.ID} already exists");
        });
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
        Engineer engineer = DataSource.Engineers.Find(ele => ele.ID == id)??
            throw new Exception($"Engineer with ID={id} not exists");
        DataSource.Engineers.Remove(engineer);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
    /// <exception cref="Exception">if the object not found</exception>
    public Engineer? Read(int id)
    {
        Engineer? eng = null;
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == id)
                eng = engineer;
        });
        return eng;
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Engineer item)
    {
        Engineer engineer = DataSource.Engineers.Find(ele => ele.ID == item.ID)??
            throw new Exception($"Engineer with ID={item.ID} not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}
