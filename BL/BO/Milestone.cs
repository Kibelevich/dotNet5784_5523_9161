
namespace BO;


/// <summary>
/// Milestone entity represents a milestone with all its properties in the business layer
/// </summary>
public class Milestone
{
    public int ID { get; init; }
    public required string description { get; set; }
    public required string alias { get; set; }
    public Status? status { get; set; }
    public DateTime createdAt { get; init; }
    public DateTime baselineStart { get; set; }
    public DateTime? start { get; set; }
    public DateTime? forecastEndDate { get; set; }
    public DateTime deadline { get; init; }
    public DateTime? complete { get; set; }
    public int? completionPercentage { get; set; }
    public string? remarks { get; set; }
    public IEnumerable<TaskInList?> dependencies { get; set; } = new List<TaskInList?>();
    public override string ToString() => Tools.ToStringProperty(this);

}
