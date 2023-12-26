using BlApi;
namespace BlImplementation;

internal class TaskInListImplementation : ITaskInList
{
    private static DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.TaskInList? Read(int? id)
    {
        if (id == null)
            return null;
        DO.Task? task = _dal.Task.Read((int)id);
        if (task == null)
            return null;
        return new BO.TaskInList()
        {
            ID = task.ID,
            description = task.desciption,
            alias = task.alias,
            status = null
        };
    }
}
