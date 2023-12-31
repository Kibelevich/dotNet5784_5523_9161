

using BlApi;
using BO;

namespace BlImplementation;

internal class MilestoneInTaskImplementation : IMilestoneInTask
{
    public MilestoneInTask? Read(int ID)
    {
        IBl bl = Factory.Get();
        BO.Milestone? milestone = bl.Milestone.Read(ID);
        if (milestone == null)
            return null;
        return new BO.MilestoneInTask()
        {
            ID = milestone.ID,
            alias = milestone.alias
        };
    }
}
