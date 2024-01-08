namespace DalApi;
using DO;

/// <summary>
/// Interface of Dependency  in DAL
/// </summary>
public interface IDependency:ICrud<Dependency>
{ 
    bool isDepend(int dependentTask, int dependsOnTask); //Checks if the dependency exists
}

