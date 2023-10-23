using Dal;
using DalApi;
using DO;

namespace DalTest//  ;????????
{
    //  למה קימות המתודות create... אם הם יאותחלו בinitialization?
    // האם לעשות לכל תת בחירה מתודה נפרדת?
    //  האם לעשות switch בתוך  switch?
    //  יש כזה דבר משתנה אנונימי?
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
            // ??????????האם צריך לעשות כזאת מתודה לכל ישות ולכל מתודה
            int _id, _level;
            string _name, _email;
            double _cost;
            Console.WriteLine("Enter id, name, email, level, cost");
            _id = Console.Read();
            _name = Console.ReadLine();
            _email = Console.ReadLine();
            _level = Console.Read();
            _cost = Console.Read();
            Engineer newEngineer = new(_id, _name, _email, (DO.EngineerExperiece)_level, _cost);
            s_dalEngineer.Create(newEngineer);
        }
        private static void readEngineer() 
        {
            Console.WriteLine("Enter engineer's ID");
            int _id = Console.Read();
            Console.WriteLine(s_dalEngineer.Read(_id));
        }

        private static void readAllEngineers() 
        {
            Console.WriteLine(s_dalEngineer.ReadAll());
        }

        private static void updateEngineer()
        {
            int _id, _level;
            string _name, _email;
            double _cost;
            Console.WriteLine("Enter id, name, email, level, cost");
            _id = Console.Read();
            _name = Console.ReadLine();
            _email = Console.ReadLine();
            _level = Console.Read();
            _cost = Console.Read();
            Engineer newEngineer = new(_id, _name, _email, (DO.EngineerExperiece)_level, _cost);
            s_dalEngineer.Update(newEngineer);
        }

        private static void deleteEngineer()
        {
            Console.WriteLine("Enter engineer's ID");
            int _id = Console.Read();
            s_dalEngineer.Delete(_id);
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
                    {
                        createEngineer();
                        break;
                    }
                    case 2:
                    {
                        readEngineer();
                        break;
                    }
                    case 3:
                    {
                        readAllEngineers();
                        break;
                    }
                    case 4:
                    {
                        updateEngineer();
                        break;
                    }
                    case 5:
                    {
                        deleteEngineer();
                        break;
                    }
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

    
