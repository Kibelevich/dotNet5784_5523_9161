
namespace DalTest;
using DalApi;
using DO;
using System.Reflection.Metadata.Ecma335;

public static class Initialization
{
    //private static IEngineer? s_dalEngineer;
    //private static ITask? s_dalTask;
    //private static IDependency? s_dalDependency;
    private static IDal? s_dal;
    private static readonly Random s_rand = new Random();

    // inits the engineer's list with random instances
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
                _id = s_rand.Next(100000000, 1000000000);
            while (s_dal!.Engineer.Read(_id) != null);
            string _email= $"{engineer.Split(' ')[0]}{_id%1000}@gmail.com";
            int _level = _id % 5;
            Engineer newEngineer = new(_id, engineer, _email, (EngineerExperiece)_level, 0);
            s_dal.Engineer!.Create(newEngineer);
        }
    }

    // inits the task's list with random instances
    public static void createTask()
    {
        IEnumerable<Engineer> engineers = s_dal!.Engineer!.ReadAll(ele => { return true; })!;
        for (int i = 0; i < 100; i++) {
            bool _milestone = s_rand.NextInt64()%2 == 0;
            DateTime _createdAt = DateTime.Today.AddDays(s_rand.Next(365));
            DateTime _deadline = _createdAt.AddDays(s_rand.Next(500));
            int _engineerId = engineers.ElementAt(s_rand.Next(40)).ID;
            int _complexityLevel = s_rand.Next(1, 5);
            Task newTask = new(0, null, null, _milestone, _createdAt, null, null,
                _deadline, null, null, null, _engineerId, (EngineerExperiece)_complexityLevel);
            s_dal.Task!.Create(newTask);
        }
    }

    // inits the dependency's list with random instances
    public static void createDependency()
    {
        int _dependentTask, _dependsOnTask;
        for (int i = 0;i < 250;i++) {
            do
            {
                _dependentTask = s_rand.Next(100);
                _dependsOnTask = s_rand.Next(_dependentTask);
            }
            while (s_dal!.Dependency!.isDepend(_dependentTask, _dependsOnTask));
            Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
            s_dal.Dependency!.Create(newDependency);
        }
    }

    // inits the lists with random instances
    public static void Do(IDal dal)
    {
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!");
        createEngineer();
        createTask();
        createDependency();
    }
}


