
namespace Dal;



internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextIdT { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdT"); }
    internal static int NextIdD { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextIdD"); }
}
