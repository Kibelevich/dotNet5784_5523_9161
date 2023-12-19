
namespace BlApi;

public interface IMilestone
{
    public BO.Milestone? Read(int id); //Reads entity object by its ID 
    public void Update(BO.Milestone item); //Updates entity object 

}
