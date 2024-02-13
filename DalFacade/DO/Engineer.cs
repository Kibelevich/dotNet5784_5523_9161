namespace DO;
/// <summary>
/// Engineer Entity represents an engineer with all its properties
/// </summary>
/// <param name="ID">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Private name of the engineer</param>
/// <param name="Email">Email of the engineer</param>
/// <param name="Level">Level of the engineer</param>
/// <param name="Cost">salary of the engineer</param>
public record Engineer(
    int ID,
    string Name,
    string Email,
    EngineerExperiece Level,
    double? Cost)
{
    public Engineer() : this(0,"","",(EngineerExperiece)0,0) { } //empty ctor

    public DateTime RegistrationDate => DateTime.Now; //get only

    // ToString function
    public override string ToString()
    {
        return "Engineer:  ID: " + ID + "  name: " + Name + "  email: " + Email + "  level: " + Level
            + "  cost: " + Cost + "\n";
    }
}
