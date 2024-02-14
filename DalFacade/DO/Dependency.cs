

namespace DO;
/// <summary>
/// An entity that describes dependencies between tasks
/// </summary>
/// <param name="ID">A unique identifier for the dependency</param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependsOnTask">Previous assignment ID number</param>
public record Dependency(
    int ID,
    int? DependentTask,
    int? DependsOnTask
)

{
    public Dependency() : this(0,0,0) { } //empty ctor 

    public DateTime RegistrationDate => DateTime.Now; //get only

    // ToString function
    public override string ToString()
    {
        return "ID: " + ID + "  pending task: " + DependentTask + "  previous task: " + DependsOnTask + "\n";
    }
}

