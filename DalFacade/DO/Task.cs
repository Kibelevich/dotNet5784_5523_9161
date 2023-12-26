

namespace DO;
/// <summary>
/// Task entity represents a task with all its props
/// </summary>
/// <param name="ID">Task unique ID</param>
/// <param name="description">Description of the task</param>
/// <param name="alias">Alias of the task</param>
/// <param name="milestone">Milestone of the task</param>
/// <param name="createdAt">The date of creating the task</param>
/// <param name="start">Start date of the task</param>
/// <param name="forecastDate">Estimated completion date of the task</param>
/// <param name="deadline">Last date for completing the task</param>
/// <param name="complete">Actual assignment completion date</param>
/// <param name="deliverable">A string describing the product</param>
/// <param name="remarks">Remarks of the task</param>
/// <param name="engineerId">The engineer ID assigned to the task</param>
/// <param name="complexityLevel">Difficulty level of a task</param>
public record Task(
    int ID,
    string description,
    string alias,
    bool milestone,
    DateTime createdAt,
    DateTime? start,
    DateTime? forecastDate,
    DateTime deadline,
    DateTime? complete,
    string? deliverable,
    string? remarks,
    int? engineerId,
    EngineerExperiece? complexityLevel
    )
{
    public Task() : this(0,"","",false, DateTime.MinValue, DateTime.MinValue,
        DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,"", "", 0,
        (EngineerExperiece)0) { } //empty ctor 
    public DateTime RegistrationDate => DateTime.Now; //get only
    public override string ToString()
    {
        return "Task:  ID: " + ID + "  description: " + description + "  alias: " + alias + "  milestone: " + milestone
            + "  created at: " + createdAt + "  start: " + start + "  forecast date: " + forecastDate + "  dead line: "
            + deadline + "  complete: " + complete + "  deliverable: " + deliverable + "  remarks: " + remarks
            + "  engineer id: " + engineerId + "  complexity level: " + complexityLevel + "\n";
    }
}
