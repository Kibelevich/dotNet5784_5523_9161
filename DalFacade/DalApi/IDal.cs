
namespace DalApi;

/// <summary>
/// A data layer object
/// </summary>
public interface IDal
{
    IEngineer Engineer { get; }
    ITask Task { get; }
    IDependency Dependency { get; }
}
