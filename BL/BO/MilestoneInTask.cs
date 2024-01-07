

namespace BO;

public class MilestoneInTask
{
    public int ID { get; init; }
    public required string alias { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
