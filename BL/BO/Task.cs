
namespace BO;

/// <summary>
/// Taskn entity represents a task with all its properties in the business layer
/// </summary>
public class Task
{
    public int ID { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; }
    public IEnumerable<TaskInList?>? DependList { get; set; } = new List<TaskInList?>();
    public MilestoneInTask? Milestone { get; set; }
    public TimeSpan? RequiredEffortTime { get; set; }
    public Status? Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? BaselineStart { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ForecastEndDate { get; set; }
    public DateTime Deadline { get; init; }
    public DateTime? Complete { get; set; }
    public string? Deliverable { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperiece ComplexityLevel { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
