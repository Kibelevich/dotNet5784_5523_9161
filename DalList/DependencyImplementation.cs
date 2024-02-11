
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;


/// <summary>
/// The implementation of dependency's CRUD methods in DAL
/// </summary>
internal class DependencyImplementation : IDependency
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
        Dependency dependency = DataSource.Dependencies.FirstOrDefault(ele => ele.ID == id)??
            throw new DalDoesNotExistException($"Dependency with ID={id} not exists");
        DataSource.Dependencies.Remove(dependency);

    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencies.FirstOrDefault(ele => ele.ID == id);
        return dependency;
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>List of all objects</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
            return DataSource.Dependencies.Where(filter);
        return DataSource.Dependencies.Select(dependency => dependency);
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Dependency item)
    {
        Dependency dependency = DataSource.Dependencies.FirstOrDefault(ele => ele.ID == item.ID)??
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }

    /// <summary>
    /// Checks if the dependency already exists
    /// </summary>
    /// <param name="_dependentTask"> The current task </param>
    /// <param name="_dependsOnTask"> The previous task </param>
    /// <returns>True if the task id depend and false if not </returns>
    public bool isDepend(int _dependentTask, int _dependsOnTask)
    {
        return DataSource.Dependencies.
            Any(dependency => dependency.dependentTask == _dependentTask 
            && dependency.dependsOnTask == _dependsOnTask);
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.First(filter);
    }
}
