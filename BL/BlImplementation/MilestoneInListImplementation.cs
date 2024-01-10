using BlApi;


namespace BlImplementation;
/// <summary>
/// The implementation of milestoneInList's CRUD methods in BL
/// </summary>

internal class MilestoneInListImplementation : IMilestoneInList
{
    /// <summary>
    /// Reads entity object by its ID 
    /// </summary>
    /// <param name="ID">the object's id to read</param>
    /// <returns>The entity or null if not found</returns>
    public BO.MilestoneInList? Read(int ID)
    {
        IBl bl = Factory.Get();
        BO.Milestone? milestone = bl.Milestone.Read(ID);
        if (milestone == null)
            return null;
        return new BO.MilestoneInList()
        {
            ID = milestone.ID,
            description = milestone.description,
            alias = milestone.alias,
            status = milestone.status,  
            completionPercentage = milestone.completionPercentage
        };
    }
}
