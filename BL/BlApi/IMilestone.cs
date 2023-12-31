
namespace BlApi;

public interface IMilestone
{
    public BO.Milestone? Read(int id); //Reads entity object by its ID 
    public void Update(BO.Milestone milestone); //Updates entity object 
    public void CreateSchedual(); // Creates the project's schedual

}
