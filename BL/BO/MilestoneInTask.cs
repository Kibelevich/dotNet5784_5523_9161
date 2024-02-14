

namespace BO;

/// <summary>
/// Milestone in task entity represents a milestone with part of properties in the business layer
/// </summary>
public class MilestoneInTask
{
    public int ID { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
