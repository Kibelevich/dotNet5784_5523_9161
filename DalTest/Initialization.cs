namespace DalTest;
using DalApi;
using DO;


/// <summary>
/// Initilize the entities' lists with random properties
/// </summary>
public static class Initialization
{
    private static IDal? s_dal; 
    private static readonly Random s_rand = new Random();

    // inits the engineer's list with random instances
    private static void CreateEngineer()
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
                _id = s_rand.Next(200000000, 400000000);
            while (s_dal!.Engineer.Read(_id) != null);
            string _email = $"{engineer.Split(' ')[0]}{_id % 1000}@gmail.com";
            int _level = _id % 5+1;
            Engineer newEngineer = new(_id, engineer, _email, (EngineerExperiece)_level, null);
            s_dal.Engineer!.Create(newEngineer);
        }
    }

    // inits the task's list with random instances
    public static void CreateTask()
    {
        string[] descriptions = { "easy", "fun", "difficult", "challenging", "be careful", "good luck" };
        string[] aliases = { "add road", "built a shop", "painting", "help mother", "plan party" };
         IEnumerable<Engineer> engineers = s_dal!.Engineer!.ReadAll(ele => { return true; })!;
        for (int i = 0; i < 100; i++)
        {
            string _description = descriptions[s_rand.Next(6)];
            string _alias = aliases[s_rand.Next(5)];
            TimeSpan span = new(s_rand.Next(300), s_rand.Next(24), s_rand.Next(60), s_rand.Next(60));
            TimeSpan _requiredEffortTime = span / 2;
            DateTime _createdAt = DateTime.Today;
            DateTime? _start = s_rand.Next(3) > 1 ? DateTime.Today + span/10 : null;
            DateTime? _forecastEndDate = _start == null ? null : _start + _requiredEffortTime;
            DateTime _deadline = _createdAt + span;
            DateTime _baselineStart = _createdAt + span / 20;
            int _complexityLevel = s_rand.Next(1, 6);
            Engineer? engineer = engineers.FirstOrDefault(eng => (int)eng.Level == _complexityLevel);
            int? _engineerId = engineer == null ? null : engineer.ID;
            Task newTask = new(0, _description, _alias, false, _requiredEffortTime, _createdAt, _baselineStart, _start,
                _forecastEndDate, _deadline, null, null, null, _engineerId, (EngineerExperiece)_complexityLevel);
            s_dal.Task!.Create(newTask);
        }
    }

    // inits the dependency's list with random instances
    public static void CreateDependency()
    {
        int _dependentTask, _dependsOnTask;
        for (int i = 0; i < 250; i++) {
            do
            {
                _dependentTask = s_rand.Next(100);
                _dependsOnTask = s_rand.Next(_dependentTask);
            }
            while (s_dal!.Dependency!.isDepend(_dependentTask, _dependsOnTask));
            Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
            s_dal!.Dependency!.Create(newDependency);
        }
    }

    // inits the lists with random instances
    public static void Do()
    {
        s_dal = Factory.Get;
        CreateEngineer();
        CreateTask();
        CreateDependency();
    }
}


