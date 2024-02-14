
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// The implementation of dependency's CRUD methods in DAL
/// </summary>
internal class DependencyImplementation : IDependency
{
    const string dependencyFile = "dependencies";

    /// <summary>
    /// Gets a dependency from xml
    /// </summary>
    /// <param name="dependency">The dependency to get</param>
    /// <returns>The dependency from xml </returns>
    static Dependency? GetDependency(XElement? dependency)
    {
        if (dependency == null)
            return null;
        return dependency.ToIntNullable("ID") is null ? null : new Dependency()
        {
            ID = (int)dependency.Element("ID")!,
            DependentTask = (int)dependency.Element("dependentTask")!,
            DependsOnTask = (int)dependency.Element("dependsOnTask")!,
        };
    }

    /// <summary>
    /// Creates a dependency in xml
    /// </summary>
    /// <param name="dependency">The dependency to create</param>
    /// <param name="id">Dependency's id</param>
    /// <returns>Dependencies of type Xelement</returns>
    static IEnumerable<XElement> CreateDependencyElement(Dependency dependency, int? id = null)
    {
        id = id is not null ? id : dependency.ID;
        yield return new XElement("ID", id);
        if (dependency.DependentTask is not null)
            yield return new XElement("dependentTask", dependency.DependentTask);
        if (dependency.DependsOnTask is not null)
            yield return new XElement("dependsOnTask", dependency.DependsOnTask);

    }

    /// <summary>
    /// Creates new entity object
    /// </summary>
    /// <param name="item">the object to add</param>
    /// <returns>the object's id</returns>
    public int Create(Dependency item)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        int id = Config.NextIdD;
        Dependency depend = item with { ID = id };
        XElement e = new XElement("dependency", CreateDependencyElement(depend));
        Dependencies.Add(e);
        XMLTools.SaveListToXMLElement(Dependencies, dependencyFile);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="DalDoesNotExistException">if the object does not exist</exception>
    public void Delete(int id)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        (Dependencies.Elements().
            FirstOrDefault(depend => depend.ToIntNullable("ID") == id) ??
            throw new DalDoesNotExistException($"Dependency with ID={id} not exists"))
            .Remove();
        XMLTools.SaveListToXMLElement(Dependencies, dependencyFile);
    }

    /// <summary>
    /// Checks if the dependency already exists
    /// </summary>
    /// <param name="_dependentTask"> The current task </param>
    /// <param name="_dependsOnTask"> The previous task </param>
    /// <returns>True if the task id depend and false if not </returns>
    public bool isDepend(int _dependentTask, int _dependsOnTask)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        return Dependencies.Elements().
            Any(dependency => dependency.ToIntNullable("dependentTask") == _dependentTask
            && dependency.ToIntNullable("dependsOnTask") == _dependsOnTask);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">The object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public Dependency? Read(int id)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        return GetDependency(Dependencies.Elements()
            .FirstOrDefault(ele => ele.ToIntNullable("ID") == id));
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>List of all objects</returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        if (filter != null)
            return Dependencies.Elements()
                .Where(xd => filter(GetDependency(xd)!))
                .Select(GetDependency)!;
        return Dependencies.Elements().Select(GetDependency)!;
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="DalDoesNotExistException">if object not found</exception>
    public void Update(Dependency item)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        (Dependencies.Elements().
            FirstOrDefault(depend => depend.ToIntNullable("ID") == item.ID) ??
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} not exists"))
            .Remove();
        XElement e = new XElement("dependency", CreateDependencyElement(item));
        Dependencies.Add(e);
        XMLTools.SaveListToXMLElement(Dependencies, dependencyFile);
    }

    /// <summary>
    ///  Reads entity object by filter
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>The object that met the condition</returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        return GetDependency(Dependencies.Elements()
                .FirstOrDefault(xd => filter(GetDependency(xd)!)));
    }
}