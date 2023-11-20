

namespace DO;
/// <summary>
/// An entity that describes dependencies between tasks
/// </summary>
/// <param name="ID">A unique identifier for the dependency</param>
/// <param name="dependentTask">ID number of pending task</param>
/// <param name="dependsOnTask">Previous assignment ID number</param>
public record Dependency(
    int ID,
    int? dependentTask,
    int? dependsOnTask
)

{
    public Dependency() : this(0,0,0) { } //empty ctor 

    public DateTime RegistrationDate => DateTime.Now; //get only

    public override string ToString()
    {
        return "ID: " + ID + "  pending task: " + dependentTask + "  previous task: " + dependsOnTask + "\n";
    }
}

