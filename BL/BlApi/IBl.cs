
namespace BlApi;

/// <summary>
/// A business layer object
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public IEngineerInTask EngineerInTask { get; }
    public IEngineerInList EngineerInList { get; }
    public ITaskInList TaskInList { get; }
    public IMilestoneInList MilestoneInList { get; }
    public IMilestoneInTask MilestoneInTask { get; }


}
