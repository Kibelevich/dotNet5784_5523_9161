
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextIdD;
        Dependency dependency = item with { ID = id };
        DataSource.Dependencies.Add(dependency);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="Exception">if the object not found</exception>

    public void Delete(int id)
    {
        Dependency dependency = DataSource.Dependencies.Find(ele => ele.ID == id) ??
            throw new Exception($"Dependency with ID={id} not exists");
        DataSource.Dependencies.Remove(dependency);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
    /// <exception cref="Exception">if the object not found</exception>
    public Dependency? Read(int id)
    {
        Dependency dependency = DataSource.Dependencies.Find(ele => ele.ID == id) ??
            throw new Exception($"Dependency with ID={id} not exists");
        return dependency;
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Dependency item)
    {
        Dependency dependency = DataSource.Dependencies.Find(ele => ele.ID == item.ID) ??
            throw new Exception($"Dependency with ID={item.ID} not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }


    public bool isDepend(int _dependentTask, int _dependsOnTask)
    {
        bool flag = false;
        DataSource.Dependencies.ForEach(dependency =>
        {
            if (dependency.dependentTask == _dependentTask && dependency.dependsOnTask == _dependsOnTask)
                flag = true;
        });
        return flag;
    }
}
