

namespace BO;

/// <summary>
/// Engineer in list entity represents an engineer with part of properties in the business layer
/// </summary>
public class EngineerInList
{
    public int ID { get; init; }
    public required string name { get; set; }
    public EngineerExperiece level { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
