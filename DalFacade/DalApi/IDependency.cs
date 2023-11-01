
namespace DalApi;
using DO;


public interface IDependency:ICrud<Dependency>
{ 
    bool isDepend(int dependentTask, int dependsOnTask); //Checks if the dependency exists
}

