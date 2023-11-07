namespace DO;
/// <summary>
/// Engineer Entity represents an engineer with all its props
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
    public override string ToString()
    {
        return "Engineer:  ID: " + ID + "  name: " + name + "  email: " + email + "  level: " + level
            + "  cost: " + cost + "\n";
    }
}
