
namespace BO;

/// <summary>
/// Milestone in list entity represents a milestone with part of properties in the business layer
/// </summary>
public class MilestoneInList
{
    public int ID { get; init; }
    public required string description { get; set; }
    public required string alias { get; set; }
    public Status? status { get; set; }
    public int? completionPercentage { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}