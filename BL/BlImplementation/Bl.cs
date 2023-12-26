
using BlApi;
namespace BlImplementation;

internal class Bl : IBl
{
    public IEngineer Engineer =>  new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IMilestone Milestone => new MilestoneImplementation();
    public IEngineerInTask EngineerInTask =>  new EngineerInTaskImplementation();
    public ITaskInList TaskInList => new TaskInListImplementation();
    public IMilestoneInList MilestoneInList => new MilestoneInListImplementation();
    public IMilestoneInTask MilestoneInTask => new MilestoneInTaskImplementation();
}
