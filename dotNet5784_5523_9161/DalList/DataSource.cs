
namespace Dal;

internal static class DataSource
{
    internal static List<DO.Engineer> Engineers = new();
    internal static List<DO.Task> Tasks = new();
    internal static List<DO.Dependency> Dependencies = new();
    internal static class Config
    {
        internal const int startId = 1;
        private static int nextId = startId;
        internal static int NextId  { get => nextId++; }
    }

}
