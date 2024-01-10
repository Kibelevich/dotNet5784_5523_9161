
namespace BO;

/// <summary>
/// Engineer in task entity represents an engineer with part of properties in the business layer
/// </summary>
public class EngineerInTask
{
    public int ID { get; init; }
    public required string name { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
