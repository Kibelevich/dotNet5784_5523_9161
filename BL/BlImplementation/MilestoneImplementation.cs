using BlApi;

namespace BlImplementation;

/// <summary>
/// The implementation of milestone's CRUD methods in BL
/// </summary>

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private IBl bl = Factory.Get();

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="id">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    public BO.Milestone Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Milestone with ID={id} does not exist");
        return ReplaceTaskToMilestone(doTask);
    }

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Returns the entity that establishes the condition </returns>
    /// <exception cref="BO.BlDoesNotExistException">if the entity does not exist</exception>
    public BO.Milestone Read(Func<BO.Milestone,bool> filter)
    {
        BO.Milestone milestone = bl.Milestone.ReadAll().FirstOrDefault(filter)??
          throw new BO.BlDoesNotExistException($"There is no milestone with such a condition");
        return milestone;
    }

    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <returns>Collection of all objects</returns>
    public IEnumerable<BO.Milestone> ReadAll()
    {
        return _dal.Task.ReadAll().Where(task => task != null && task.Milestone).Select(task => ReplaceTaskToMilestone(task!));
    }

    /// <summary>
    /// Updates entity object
    /// </summary>
    /// <param name="milestone">The object to update</param>
    /// <exception cref="BO.BlIllegalPropertyException">if the properties are illegal</exception>
    /// <exception cref="BO.BlDoesNotExistException">if object not found</exception>
    public void Update(BO.Milestone milestone)
    {
        if (milestone.alias == "")
            throw new BO.BlIllegalPropertyException($"Invalid property");
        DO.Task doTask = _dal.Task.Read(milestone.ID) ??
                        throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists");
        try
        {
            _dal.Task.Delete(milestone.ID);
            DO.Task updatedTask = doTask
                with
            { Alias = milestone.alias, Description = milestone.description, Remarks = milestone.remarks };
            int id = _dal.Task.Create(updatedTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={milestone.ID} does not exists", ex);
        }
    }

    /// <summary>
    /// Replace the entity from task object to milestone object
    /// </summary>
    /// <param name="task">the task object to replace</param>
    /// <returns>the milestone object</returns>
    BO.Milestone ReplaceTaskToMilestone(DO.Task task)
    {
        IEnumerable<BO.TaskInList?> taskInLists = DependList(task.ID);
        return new BO.Milestone()
        {
            ID = task.ID,
            description = task.Description,
            alias = task.Alias,
            status = CalcStatus(task),
            createdAt = task.CreatedAt,
            start = task.Start,
            forecastEndDate = task.ForecastEndDate,
            deadline = task.Deadline,
            complete = task.Complete,
            remarks = task.Remarks,
            completionPercentage = CalcCompletionPercentage(taskInLists),
            dependencies = taskInLists
        };
    }
    /// <summary>
    /// Calculets the status of the task
    /// </summary>
    /// <param name="doTask">The task for calculation</param>
    /// <returns></returns>
    /// <exception cref="BO.BlDeadlinePassedException">if the dead line passed</exception>
    BO.Status CalcStatus(DO.Task doTask)
    {
        DateTime now = DateTime.Now;
        if (doTask.Complete < now) return (BO.Status)4;
        if (doTask.ForecastEndDate < now && doTask.Deadline > now) return (BO.Status)3;
        if (doTask.Start <= now) return (BO.Status)2;
        if (doTask.BaselineStart != null) return (BO.Status)1;
        return 0;
    }

    /// <summary>
    /// Calculates the dependencies list
    /// </summary>
    /// <param name="ID">the task's id</param>
    /// <returns>the list of dependencies</returns>
    IEnumerable<BO.TaskInList?> DependList(int ID)
    {
        return _dal.Dependency.ReadAll(depend => depend.dependentTask == ID)
            .Select(depend => depend == null ? null : bl.TaskInList.Read(depend.dependsOnTask));
    }

    /// <summary>
    /// Calculets the completion percentage of the milestone
    /// </summary>
    /// <param name="tasksInList">dependenies' list</param>
    /// <returns>The completion percentage</returns>
    int? CalcCompletionPercentage(IEnumerable<BO.TaskInList?> tasksInList)
    {
        int tasksAmount = tasksInList.Count();
        int doneTasks = tasksInList.Count(t => t != null && t.status == (BO.Status)4);
        return (100 * doneTasks) / tasksAmount;
    }

    public void CreateSchedual()
    {
        CreateSchedual(DateTime.MinValue, DateTime.MaxValue);
    }

    public void CreateSchedual(DateTime startProject, DateTime endProject)
    {
        CreateMilestones();
        calcTimes(startProject, endProject);
    }

    void CreateMilestones()
    {
        int nextId = 1;
        _dal.Task.ReadAll(task => _dal.Dependency.ReadAll(depend => depend.dependentTask == task.ID) == null)
            .Where(task => task != null).Select(task => CreateStartMilestone(task!.ID));
        IEnumerable<(int? key, IEnumerable<DO.Dependency> dependencies)> list =
            (from dependency in _dal.Dependency.ReadAll()
             group dependency by dependency.dependentTask into dependGroup
             orderby dependGroup.Key
             select (dependGroup.Key, dependGroup.Select(depend => depend)));
        IEnumerable<int> milestoneIDs = list
            .Select(task => CreateMilestone(task.key, task.dependencies, nextId++));
        _dal.Task.ReadAll(task => _dal.Dependency.ReadAll(depend => depend.dependsOnTask == task.ID) == null)
            .Where(task => task != null).Select(task => CreateEndMilestone(task!.ID));
    }

    int CreateMilestone(int? key, IEnumerable<DO.Dependency> dependencies, int nextId)
    {
        int? id;
        id = (from milestone in bl.Milestone.ReadAll()
              where milestone.dependencies.Any(depend1 => depend1 != null ? dependencies.Any(depend2 => depend2.dependsOnTask == depend1.ID) : false)
              select milestone.ID).FirstOrDefault();
        if (id == null)
        {
            id = _dal.Task.Create(new DO.Task(0, $"Milestone number: {nextId}", $"M{nextId}", true,
                TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, "", "", null, 0));
        }
        IEnumerable<int> dependIDs = from DO.Dependency dependTask in dependencies
                                     select _dal.Dependency.Create(dependTask with { dependentTask = id });
        var i = from DO.Dependency dependTask in dependencies
                where dependTask.dependentTask == key
                select DeleteDependency(dependTask.ID);
        _dal.Dependency.Create(new DO.Dependency(0, key, id));
        return (int)id;
    }

    int CreateStartMilestone(int taskID)
    {
        if (bl.Milestone.Read(milestone => milestone.alias == "START") == null)
            _dal.Task.Create(new DO.Task(0, "Start milestone", "START", true,
               TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
               DateTime.MinValue, "", "", null, 0));
        _dal.Dependency.Create(new DO.Dependency(0, taskID, 0));
        return 0;
    }

    int CreateEndMilestone(int taskID)
    {
        int id;
        BO.Milestone? endMilestone = bl.Milestone.Read(milestone => milestone.alias == "END");
        if (endMilestone == null)
            id = _dal.Task.Create(new DO.Task(0, "End milestone", "END", true,
                TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, "", "", null, 0));
        else
            id = endMilestone.ID;
        _dal.Dependency.Create(new DO.Dependency(0, id, taskID));
        return id;
    }

    int DeleteDependency(int id)
    {
        _dal.Dependency.Delete(id);
        return id;
    }

    void calcTimes(DateTime startProject, DateTime endProject)
    {
        BO.Milestone firstMilestone = bl.Milestone.ReadAll().First();
        bl.Milestone.Update(new BO.Milestone()
        {
            ID = firstMilestone.ID,
            alias = firstMilestone.alias,
            description = firstMilestone.description,
            status = firstMilestone.status,
            createdAt = firstMilestone.createdAt,
            baselineStart = firstMilestone.baselineStart,
            start = firstMilestone.start,
            forecastEndDate = firstMilestone.forecastEndDate,
            deadline = endProject,
            complete = firstMilestone.complete,
            completionPercentage = firstMilestone.completionPercentage,
            remarks = firstMilestone.remarks,
            dependencies = firstMilestone.dependencies
        });
        IEnumerable<BO.Milestone> milestonses = bl.Milestone.ReadAll().Reverse();
        var i = from milestone in milestonses
                where milestone.alias != "START"
                let minTime = milestone.deadline - milestone.dependencies
                    .Max(depend => depend != null ? _dal.Task.Read(depend.ID)!.RequiredEffortTime : TimeSpan.Zero)
                select (milestone.dependencies.Select(depend =>
                {
                    DO.Task? task = depend == null ? null : _dal.Task.Read(depend.ID);
                    if (task != null)
                        _dal.Task.Update(task with { Deadline = milestone.deadline });
                    return task;
                }));
    }

}
