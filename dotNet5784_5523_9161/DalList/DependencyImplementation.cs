
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextIdD;
        Dependency dependency = item with { ID = id };
        DataSource.Dependencies.Add(dependency);
        return id;
    }

    public void Delete(int id)
    {
        Dependency? dependency = DataSource.Dependencies.Find(ele => ele.ID == id);
        if (dependency == null) 
            throw new Exception($"Dependency with ID={id} not exists");
        DataSource.Dependencies.Remove(dependency);
    }

    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencies.Find(ele => ele.ID == id);
        if (dependency == null)
            throw new Exception($"Dependency with ID={id} not exists");
        return dependency;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency dependency = DataSource.Dependencies.Find(ele => ele.ID == item.ID);
        if(dependency == null)
            throw new Exception($"Dependency with ID={item.ID} not exists");
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }


    // לא בטוח שצריך אולי קשור לשכבה אחרת
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
