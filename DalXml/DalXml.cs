

namespace Dal;
using DalApi;

/// <summary>
///  A data layer object using xml
/// </summary>
sealed internal class DalXml : IDal
{
    /// <summary>
    /// So that the object is initialized only on the first use we used Lazy
    /// and so that 2 entities cannot use it at the same time we sent true to the constructor which makes it thread safe
    /// </summary>
    public static IDal Instance { get; } = new Lazy<DalXml>(()=>new DalXml(),true).Value;

    // empty ctor for DalXml
    private DalXml() { }
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
