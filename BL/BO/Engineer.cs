
namespace BO;


/// <summary>
/// Engineer Entity represents an engineer with all its properties in the business layer
/// </summary>
public class Engineer
{
    public int ID { get; init; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public EngineerExperiece Level { get; set; }
    public  double? Cost { get; set; }
    public TaskInEngineer? CurrentTask { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);
}
