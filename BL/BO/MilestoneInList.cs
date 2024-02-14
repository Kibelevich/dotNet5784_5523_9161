
namespace BO;

/// <summary>
/// Milestone in list entity represents a milestone with part of properties in the business layer
/// </summary>
public class MilestoneInList
{
    public int ID { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public Status? Status { get; set; }
    public int? CompletionPercentage { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}