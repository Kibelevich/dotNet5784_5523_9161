
namespace Dal;

internal static class DataSource
{
    internal static List<DO.Engineer> Engineers = new();
    internal static List<DO.Task> Tasks = new();
    internal static List<DO.Dependency> Dependencies = new();
    internal static class Config
    {
        internal const int startId = 1;
        private static int nextIdT = startId;
        private static int nextIdD = startId;
        internal static int NextIdT  { get => nextIdT++; }
        internal static int NextIdD { get => nextIdD++; }

    }
}
