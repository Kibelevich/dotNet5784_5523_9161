
namespace DalTest;
using DalApi;
using DO;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;
    private static readonly Random s_rand = new();

    private static void createEngineer()
    {
        string[] engineerName =
        {
            "Dani Levi",
            "Eli Amar",
            "Yair Cohen",
            "Ariela Levin",
            "Dina Klein",
            "Shira Israelof",
            "Maly Kibelewich",
            "Devory Mimran",
            "Moshe Cohen",
            "Yossef Catz",
            "Ronen Lev",
            "Ruth Salomon",
            "Ayala Schreiber",
            "Rivka Sorcher",
            "Leah Shitrit",
            "Chana Winer",
            "Shalom Malul",
            "Itschak Levi",
            "Anat Shapira",
            "Lion Bachar",
            "Daniel Ochana",
            "Shmuel Levinger",
            "Aharon Shif",
            "Tamar Druk",
            "Malca Bruk",
            "Ruti Maman",
            "Miriam Kaner",
            "Sara Shraga",
            "Reuven Lin",
            "Netanel Telem",
            "Shoshana Grilak",
            "Israel Cohen",
            "Noa Yashar",
            "Noam Heler",
            "Tzion Harush",
            "Michal Zusman",
            "Chaya Chazan",
            "Riki Mozes",
            "Hadas Dayan",
            "Avigail Safra"
    };
        foreach (var engineer in engineerName)
        {
            int _id;
            do
                _id = s_rand.Next(100000000, 999999999);
            while (s_dalEngineer!.Read(_id) != null);

        }
    }
}
