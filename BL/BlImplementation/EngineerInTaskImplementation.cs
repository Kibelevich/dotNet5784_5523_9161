
using BlApi;
namespace BlImplementation;

internal class EngineerInTaskImplementation : IEngineerInTask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.EngineerInTask? Read(int? ID)
    {
        if (ID == null)
            return null;
        DO.Engineer? engineer = _dal.Engineer.Read((int)ID)??
            throw new BO.BlDoesNotExistException($"Engineer in this task does not exists");
            return new BO.EngineerInTask() { ID=engineer.ID, name= engineer.name };
    }
}
