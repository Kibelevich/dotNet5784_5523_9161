
namespace BIApi;

public interface IEngineer
{
    public int Create(BO.Engineer item); //Creates new entity object
    public BO.Engineer? Read(int id); //Reads entity object by its ID 
    // public BO.Engineer? Read(Func<BO.Engineer, bool> filter); // Reads entity object by filter
    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null); //Reads all entity objects by condition
    public void Update(BO.Engineer item); //Updates entity object
    public void Delete(int id); //Deletes an object by its Id
}
