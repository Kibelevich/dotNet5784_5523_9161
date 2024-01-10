namespace Dal;
using DalApi;

/// <summary>
///  A data layer object using lists
/// </summary>
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IEngineer Engineer =>  new EngineerImplementation();
    public ITask Task => new TaskImplementation();
    public IDependency Dependency => new DependencyImplementation();
}
