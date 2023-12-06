

namespace Dal;
using DalApi;
using System.Diagnostics;

sealed internal class DalXml : IDal
{
    /// <summary>
    /// So that the object is initialized only on the first use we used Lazy
    /// and so that 2 entities cannot use it at the same time we sent true to the constructor which makes it thread safe
    /// </summary>
    public static IDal Instance { get; } = new Lazy<DalXml>(true).Value;
    private DalXml() { }
    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
