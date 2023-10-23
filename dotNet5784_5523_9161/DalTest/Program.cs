using Dal;
using DalApi;


namespace DalTest
{
    internal class Program
    {
        private static IEngineer? s_dalEngineer = new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();
        private static IDependency? s_dalDependency = new DependencyImplementation();

        private static void mainMenu()
        {
            Console.WriteLine("Choose:\n 0 to exit\n 1 to engineer\n 2 to task\n 3 to dependency\n");
        }

        private static void mainSubmenu(string entity)
        {    
            Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
            $"3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n");
        }
        static void Main(string[] args)
        {

            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            int choice;
            do {
                mainMenu();
                choice = Console.Read();
            } while (choice);
        }

    }
}

    
