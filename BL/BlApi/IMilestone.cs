
namespace BlApi;

public interface IMilestone
{
    public BO.Milestone? Read(int id); //Reads entity object by its ID 
    public IEnumerable<BO.Milestone> ReadAll(); // Reads all the entities
    public BO.Milestone? Read(Func<BO.Milestone,bool> filter); //Reads the entity by filter
    public void Update(BO.Milestone milestone); //Updates entity object 
    public void CreateSchedual(); // Creates the project's schedual

}
