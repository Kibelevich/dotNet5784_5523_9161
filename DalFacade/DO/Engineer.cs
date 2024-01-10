namespace DO;
/// <summary>
/// Engineer Entity represents an engineer with all its properties
/// </summary>
/// <param name="ID">Personal unique ID of engineer (as in national id card)</param>
/// <param name="name">Private name of the engineer</param>
/// <param name="email">Email of the engineer</param>
/// <param name="level">Level of the engineer</param>
/// <param name="cost">salary of the engineer</param>
public record Engineer(
    int ID,
    string name,
    string email,
    EngineerExperiece level,
    double cost)
{
    public Engineer() : this(0,"","",(EngineerExperiece)0,0) { } //empty ctor

    public DateTime RegistrationDate => DateTime.Now; //get only

    // ToString function
    public override string ToString()
    {
        return "Engineer:  ID: " + ID + "  name: " + name + "  email: " + email + "  level: " + level
            + "  cost: " + cost + "\n";
    }
}
