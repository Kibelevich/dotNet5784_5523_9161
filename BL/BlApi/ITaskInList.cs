
namespace BlApi;

/// <summary>
/// Interface of task in list in BL
/// </summary>

public interface ITaskInList
{
    public BO.TaskInList? Read(int? id); //Reads entity object by its ID 
    public IEnumerable<BO.TaskInList?> ReadAll(Func<BO.TaskInList, bool>? filter = null); //Reads all entity objects by condition
}
