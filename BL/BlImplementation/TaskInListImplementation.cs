using BlApi;
namespace BlImplementation;

/// <summary>
/// The implementation of taskInList's CRUD methods in BL
/// </summary>

internal class TaskInListImplementation : ITaskInList
{
    private static DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
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


    /// <summary>
    /// Calculates the task's status according to the dates
    /// </summary>
    /// <param name="doTask">The task to calculate for</param>
    /// <returns>The task's status </returns>
    /// <exception cref="BO.BlDeadlinePassedException">If the dead line passed</exception>
    BO.Status calcStatus(DO.Task doTask)
    {
        DateTime now = DateTime.Now;
        if (doTask.complete < now) return (BO.Status)4;
        if (doTask.deadline < now) throw new BO.BlDeadlinePassedException($"Deadline passed");
        if (doTask.forecastEndDate < now && doTask.deadline > now) return (BO.Status)3;
        if (doTask.start < now) return (BO.Status)2;
        if (doTask.deadline < now) return (BO.Status)1;
        return 0;
    }

}
