
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string dependencyFile = "dependencies";

    static Dependency? getDependency(XElement dependency) =>
        dependency.ToIntNullable("ID") is null ? null : new DO.Dependency()
        {
            ID = (int)dependency.Element("ID")!,
            dependentTask = (int)dependency.Element("dependentTask")!,
            dependsOnTask = (int)dependency.Element("dependsOnTask")!,
        };

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
        int id = Config.NextIdD;
        XMLTools.SaveListToXMLElement(createDependencyElement(item, id), dependencyFile);
        return id;
    }

    /// <summary>
    /// Deletes an object by its Id
    /// </summary>
    /// <param name="id">the object's id to delete</param>
    /// <exception cref="Exception">if the object not found</exception>
    public void Delete(int id)
    {
        XElement dependencyElement = XMLTools.LoadListFromXMLElement(dependencyFile);
        (dependencyElement.Elements().
            FirstOrDefault(depend => (int?)depend.Element("ID") == id) ??
            throw new DalDoesNotExistExeption($"Dependency with ID={id} not exists"))
            .Remove();
    }

    public bool isDepend(int _dependentTask, int _dependsOnTask)
    {
        XElement Dependencies = XMLTools.LoadListFromXMLElement(dependencyFile);
        return Dependencies.Elements().
            Any(dependency => (int?)dependency.Element("dependentTask") == _dependentTask
            && (int?)dependency.Element("dependsOnTask") == _dependsOnTask);
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
            .FirstOrDefault(ele => (int?)ele.Element("ID") == id)!);
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
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
        Dependency dependency = DataSource.Dependencies.FirstOrDefault(ele => ele.ID == item.ID) ??
            throw new DalDoesNotExistExeption($"Dependency with ID={item.ID} not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
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

