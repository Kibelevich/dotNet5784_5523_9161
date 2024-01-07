
namespace BO;

public class EngineerInTask
{
    public int ID { get; init; }
    public required string name { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
