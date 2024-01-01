
namespace BO;

public class Task
{
    public int ID { get; init; }
    public required string description{ get; set; }
    public required string alias { get; set; }
    public IEnumerable<TaskInList?> dependList { get; set; } = new List<TaskInList?>();
    public MilestoneInTask? milestone { get; set; }
    public TimeSpan requiredEffortTime { get; set; }
    public Status? status { get; set; }
    public DateTime createdAt { get; init; }
    public DateTime? baselineStart { get; set; }
    public DateTime? start { get; set; }
    public DateTime? forecastEndDate { get; set; }
    public DateTime deadline { get; init; }
    public DateTime? complete { get; set; }
    public string? deliverable { get; set; }
    public string? remarks { get; set; }
    public EngineerInTask? engineer { get; set; }
    public EngineerExperiece? complexityLevel { get; set; }
}
