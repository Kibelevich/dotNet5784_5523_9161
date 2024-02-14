
namespace BO;

/// <summary>
/// Task in engineer entity represents a task with part of properties in the business layer
/// </summary>
public class TaskInEngineer
{
    public int ID { get; init; }
    public required string Alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
