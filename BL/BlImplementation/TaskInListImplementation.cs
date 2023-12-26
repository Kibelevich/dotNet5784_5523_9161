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
            description = task.description,
            alias = task.alias,
            status = calcStatus(task)
        };
    }

    BO.Status calcStatus(DO.Task doTask)
    {
        DateTime now = DateTime.Now;
        if (doTask.complete < now) return (BO.Status)4;
        if (doTask.deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.forecastDate < now && doTask.deadline > now) return (BO.Status)3;
        if (doTask.start < now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }

}
