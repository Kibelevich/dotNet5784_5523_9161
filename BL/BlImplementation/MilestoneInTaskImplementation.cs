

using BlApi;

namespace BlImplementation;

/// <summary>
/// The implementation of milestoneInTask's CRUD methods in BL
/// </summary>

internal class MilestoneInTaskImplementation : IMilestoneInTask
{

    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="ID">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public BO.MilestoneInTask? Read(int ID)
    {
        IBl bl = Factory.Get();
        BO.Milestone? milestone = bl.Milestone.Read(ID);
        if (milestone == null)
            return null;
        return new BO.MilestoneInTask()
        {
            ID = milestone.ID,
            Alias = milestone.Alias
        };
    }
}
