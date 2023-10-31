
namespace DalApi;
using DO;


public interface IDependency
{ 
    int Create(Dependency item); //Creates new entity object
    Dependency? Read(int id); //Reads entity object by its ID 
    List<Dependency> ReadAll(); //Reads all entity objects
    void Update(Dependency item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
    bool isDepend(int dependentTask, int dependsOnTask); //Checks if the dependency exists
}

