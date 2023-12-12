
namespace BIApi;

public interface IBI
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public IMilestone Milestone { get; }
    public IEngineerInTask EngineerInTask { get; }
    public ITaskInList TaskInList { get; }
    public IMilestoneInList MilestoneInList { get; }

}
