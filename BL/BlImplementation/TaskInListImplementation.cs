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
            Description = task.Description,
            Alias = task.Alias,
            Status = CalcStatus(task)
        };
    }

    /// <summary>
    /// Reads all entity objects by condition
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Collection of all objects</returns>
    public IEnumerable<BO.TaskInList?> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        if (filter != null)
        {
            return from DO.Task doTask in _dal.Task.ReadAll()
                   let boTaskInList = new BO.TaskInList()
                   {
                       ID = doTask.ID,
                       Description = doTask.Description,
                       Alias = doTask.Alias,
                       Status = CalcStatus(doTask),
                   }
                   where filter(boTaskInList)
                   orderby boTaskInList.ID
                   select boTaskInList;
        }
        return from DO.Task doTask in _dal.Task.ReadAll()
               orderby doTask.ID
               select new BO.TaskInList()
               {
                   ID = doTask.ID,
                   Description = doTask.Description,
                   Alias = doTask.Alias,
                   Status = CalcStatus(doTask),
               };
    }

    /// <summary>
    /// Calculates the task's status according to the dates
    /// </summary>
    /// <param name="doTask">The task to calculate for</param>
    /// <returns>The task's status </returns>
    /// <exception cref="BO.BlDeadlinePassedException">If the dead line passed</exception>
    BO.Status CalcStatus(DO.Task doTask)
    {
        DateTime now = DateTime.Now;
        if (doTask.Complete < now) return (BO.Status)4;
        if (doTask.ForecastEndDate < now && doTask.Deadline > now) return (BO.Status)3;
        if (doTask.Start <= now) return (BO.Status)2;
        if (doTask.BaselineStart != null) return (BO.Status)1;
        return 0;
    }
}
