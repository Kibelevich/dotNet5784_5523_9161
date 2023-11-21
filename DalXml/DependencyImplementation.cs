
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string dependencyFile = "dependencies";

    static Dependency? getDependency(XElement? dependency)
    {
        if (dependency == null)
            return null;
        return dependency.ToIntNullable("ID") is null ? null : new Dependency()
        {
            ID = (int)dependency.Element("ID")!,
            dependentTask = (int)dependency.Element("dependentTask")!,
            dependsOnTask = (int)dependency.Element("dependsOnTask")!,
        };
    }


    static IEnumerable<XElement> createDependencyElement(Dependency dependency, int? id = null)
    {
        id = id is not null ? id : dependency.ID;
        yield return new XElement("ID", id);
        if (dependency.dependentTask is not null)
            yield return new XElement("dependentTask", dependency.dependentTask);
        if (dependency.dependsOnTask is not null)
            yield return new XElement("dependsOnTask", dependency.dependsOnTask);

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
        XElement e = new XElement("dependency", createDependencyElement(depend));
        Dependencies.Add(e);
        XMLTools.SaveListToXMLElement(Dependencies, dependencyFile);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="Exception">if the object not found</exception>
    public void Delete(int id)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        (Dependencies.Elements().
            FirstOrDefault(depend => depend.ToIntNullable("ID") == id) ??
            throw new DalDoesNotExistExeption($"Dependency with ID={id} not exists"))
            .Remove();
        XMLTools.SaveListToXMLElement(Dependencies, dependencyFile);
    }

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
    /// <param name="id">the object's id to read</param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        return getDependency(Dependencies.Elements()
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
                .Where(xd => filter(getDependency(xd)!))
                .Select(getDependency)!;
        return Dependencies.Elements().Select(getDependency)!;
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="item">The object to update</param>
    /// <exception cref="Exception">if object not found</exception>
    public void Update(Dependency item)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        (Dependencies.Elements().
            FirstOrDefault(depend => depend.ToIntNullable("ID") == item.ID) ??
            throw new DalDoesNotExistExeption($"Dependency with ID={item.ID} not exists"))
            .Remove();
        XElement e = new XElement("dependency", createDependencyElement(item));
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
        return getDependency(Dependencies.Elements()
                .FirstOrDefault(xd => filter(getDependency(xd)!)));
    }
}