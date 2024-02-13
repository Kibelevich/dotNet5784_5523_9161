

namespace DO;
/// <summary>
/// Task entity represents a task with all its props
/// </summary>
/// <param name="ID">Task unique ID</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">Alias of the task</param>
/// <param name="Milestone">Milestone of the task</param>
/// <param name="RequiredEffortTime">Required effort time of the task</param>
/// <param name="CreatedAt">The date of creating the task</param>
/// <param name="BaselineStart">Baseline start date of the task</param>
/// <param name="Start">Start date of the task</param>
/// <param name="ForecastEndDate">Estimated completion date of the task</param>
/// <param name="Deadline">Last date for completing the task</param>
/// <param name="Complete">Actual assignment completion date</param>
/// <param name="Deliverable">A string describing the product</param>
/// <param name="Remarks">Remarks of the task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="ComplexityLevel">Difficulty level of a task</param>
/// 
public record Task(
    int ID,
    string Description,
    string Alias,
    bool Milestone,
    TimeSpan? RequiredEffortTime,
    DateTime CreatedAt,
    DateTime? BaselineStart,
    DateTime? Start,
    DateTime? ForecastEndDate,
    DateTime Deadline,
    DateTime? Complete,
    string? Deliverable,
    string? Remarks,
    int? EngineerId,
    EngineerExperiece ComplexityLevel
    )
{

    public Task() : this(0,"","",false,TimeSpan.Zero, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,
        DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,"", "", 0,
        (EngineerExperiece)0) { } //empty ctor 
    public DateTime RegistrationDate => DateTime.Now; //get only

    // ToString function
    public override string ToString()
    {
        return "Task:  ID: " + ID + "  description: " + Description + "  alias: " + Alias + "  milestone: " + Milestone
           + "  required effort time: " + RequiredEffortTime + "  created at: " + CreatedAt + "  baseline start: " + BaselineStart
           + "  start: " + Start + "  forecast date: " + ForecastEndDate + "  dead line: " + Deadline
           + "  complete: " + Complete + "  deliverable: " + Deliverable + "  remarks: " + Remarks
           + "  engineer id: " + EngineerId + "  complexity level: " + ComplexityLevel + "\n";
    }
}
