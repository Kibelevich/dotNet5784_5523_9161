
using BlApi;

namespace BlImplementation;

internal class TaskInListImplementation : ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.TaskInList? Read(int id)
    {
        BO.Task? task = BO.Task.Read(id);
        return new BO.TaskInList()
        {
            ID = task.ID,
            description = task.desciption,
            alias = task.alias,
            status = task.status
        };
    }
}
