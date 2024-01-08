
using BlApi;
namespace BlImplementation;
/// <summary>
/// The implementation of engineerInTask's CRUD methods in BL
/// </summary>

internal class EngineerInTaskImplementation : IEngineerInTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="ID">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    public BO.EngineerInTask? Read(int? ID)
    {
        if (ID == null)
            return null;
        DO.Engineer? engineer = _dal.Engineer.Read((int)ID)??
            throw new BO.BlDoesNotExistException($"Engineer in this task does not exists");
            return new BO.EngineerInTask() { ID=engineer.ID, name= engineer.name };
    }
}
