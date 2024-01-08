
namespace BlApi;

/// <summary>
/// Interface of engineer in list  in BL
/// </summary>
public interface IEngineerInList
{
    public IEnumerable<BO.EngineerInList?> ReadAll(Func<BO.EngineerInList, bool>? filter = null); //Reads all entity objects by condition

}
