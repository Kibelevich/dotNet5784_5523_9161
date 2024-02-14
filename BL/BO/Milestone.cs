
namespace BO;


/// <summary>
/// Milestone entity represents a milestone with all its properties in the business layer
/// </summary>
public class Milestone
{
    public int ID { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public Status? Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime BaselineStart { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ForecastEndDate { get; set; }
    public DateTime Deadline { get; init; }
    public DateTime? Complete { get; set; }
    public int? CompletionPercentage { get; set; }
    public string? Remarks { get; set; }
    public IEnumerable<TaskInList?> Dependencies { get; set; } = new List<TaskInList?>();
    public override string ToString() => Tools.ToStringProperty(this);

}
