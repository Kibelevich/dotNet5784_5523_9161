using Dal;
using DalApi;
using DO;

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
            int choice;
            do
            {
                choice = Console.Read();
                switch (choice)
                {
                    case 1:
                    {
                        crudMenu("engineer");
                        crudEngineer();
                        break;
                    }
                    case 2:
                    {
                        crudMenu("task");
                        break;
                    }
                    case 3:
                    {
                        crudMenu("dependency");
                        break;
                    }
                    default:
                        break;
                }
            } while (choice > 0);
        }

        private static void createEngineer()
        {
            int _id;
            string _name, _email;
            EngineerExperiece _level;
            double _cost;
            Console.WriteLine("Enter id, name, email, level, cost");
        }
        private static void crudEngineer()
        {
            int choice;
            do
            {
                choice = Console.Read();
                switch(choice)
                {
                    case 1:
                        createEngineer();
                        break;
                        
                }
            } while (choice > 0);
            
        }
        private static void crudMenu(string entity)
        {    
            Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
            $"3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n"); 
        }
        

        static void Main(string[] args)
        {

            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
            
        }

    }
}

    
