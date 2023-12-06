
namespace BO;

public class Task
{
    public int ID { get; init; }
    public required string desciption{ get; set; }
    public required string alias { get; set; }
    public TaskInList? dependList { get; set; }
    public MilestoneInList? milestone { get; set; }
    public Status? status { get; set; }
    public DateTime createdAt { get; init; }
    public DateTime? baselineStart { get; set; }
    public DateTime? start { get; set; }
    public DateTime? forecastDate { get; set; }
    public DateTime deadline { get; init; }
    public DateTime? complete { get; set; }
    public string? deliverable { get; set; }
    public string? remarks { get; set; }
    public EngineerInTask? engineer { get; set; }
    public EngineerExperiece? complexityLevel { get; set; }
}
