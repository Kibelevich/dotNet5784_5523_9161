
namespace BO;


/// <summary>
/// Task in list entity represents a task with part of properties in the business layer
/// </summary>
public class TaskInList
{
    public int ID { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public Status? Status { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
