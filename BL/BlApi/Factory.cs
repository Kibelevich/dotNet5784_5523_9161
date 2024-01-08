

namespace BlApi;
/// <summary>
///  Class for creating a single BL object
/// </summary>
public static class Factory
{
    public static IBl Get() => new BlImplementation.Bl(); // Get the single BL object
}
