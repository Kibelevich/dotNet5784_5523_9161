
namespace BO;

public class TaskInEngineer
{
    public int ID { get; init; }
    public required string alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
