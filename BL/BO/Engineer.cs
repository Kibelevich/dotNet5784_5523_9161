
namespace BO;


/// <summary>
/// Engineer Entity represents an engineer with all its properties in the business layer
/// </summary>
public class Engineer
{
    public int ID { get; init; }
    public required string name { get; set; }
    public required string email { get; set; }
    public EngineerExperiece level { get; set; }
    public  double cost { get; set; }
    public TaskInEngineer? currentTask { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
