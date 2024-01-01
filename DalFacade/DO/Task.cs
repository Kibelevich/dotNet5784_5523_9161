

namespace DO;
/// <summary>
/// Task entity represents a task with all its props
/// </summary>
/// <param name="ID">Task unique ID</param>
/// <param name="description">Description of the task</param>
/// <param name="alias">Alias of the task</param>
/// <param name="milestone">Milestone of the task</param>
/// <param name="requiredEffortTime">Required effort time of the task</param>
/// <param name="createdAt">The date of creating the task</param>
/// <param name="baselineStart">Baseline start date of the task</param>
/// <param name="start">Start date of the task</param>
/// <param name="forecastEndDate">Estimated completion date of the task</param>
/// <param name="deadline">Last date for completing the task</param>
/// <param name="complete">Actual assignment completion date</param>
/// <param name="deliverable">A string describing the product</param>
/// <param name="remarks">Remarks of the task</param>
/// <param name="engineerId">The engineer ID assigned to the task</param>
/// <param name="complexityLevel">Difficulty level of a task</param>
/// 
public record Task(
    int ID,
    string description,
    string alias,
    bool milestone,
    TimeSpan requiredEffortTime,
    DateTime createdAt,
    DateTime? baselineStart,
    DateTime? start,
    DateTime? forecastEndDate,
    DateTime deadline,
    DateTime? complete,
    string? deliverable,
    string? remarks,
    int? engineerId,
    EngineerExperiece? complexityLevel
    )
{
    public Task() : this(0,"","",false,TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
        DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,"", "", 0,
        (EngineerExperiece)0) { } //empty ctor 
    public DateTime RegistrationDate => DateTime.Now; //get only
    public override string ToString()
    {
        return "Task:  ID: " + ID + "  description: " + description + "  alias: " + alias + "  milestone: " + milestone
           + "  required effort time: " + requiredEffortTime + "  created at: " + createdAt + "  baseline start: " + baselineStart
           + "  start: " + start + "  forecast end date: " + forecastEndDate + "  dead line: " + deadline
           + "  complete: " + complete + "  deliverable: " + deliverable + "  remarks: " + remarks
           + "  engineer id: " + engineerId + "  complexity level: " + complexityLevel + "\n";
    }
}
