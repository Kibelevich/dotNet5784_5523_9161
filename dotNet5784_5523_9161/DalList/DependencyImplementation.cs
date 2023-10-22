
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextId;
        Dependency dependency = item with { ID = id };
        DataSource.Dependencies.Add(dependency);
        return id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
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
