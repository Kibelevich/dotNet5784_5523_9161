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
        private static void createEngineer()
        {
            try { 
                int _id, _level;
                string _name, _email;
                double _cost;
                Console.WriteLine("Enter id, name, email, level, cost");
                _id = Convert.ToInt32(Console.ReadLine());
                _name = Console.ReadLine()!;
                _email = Console.ReadLine()!;
                _level = Convert.ToInt32(Console.ReadLine());
                _cost = Convert.ToInt32(Console.ReadLine());
                Engineer newEngineer = new(_id, _name, _email, (DO.EngineerExperiece)_level, _cost);
                s_dalEngineer!.Create(newEngineer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void readEngineer()
        {
            Console.WriteLine("Enter engineer's ID");
            int _id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(s_dalEngineer!.Read(_id));
        }

        private static void readAllEngineers()
        {
            s_dalEngineer!.ReadAll().ForEach(ele =>
            {
                Console.WriteLine(ele);
            });
        }

        private static void updateEngineer()
        {
            try { 
                int _id, _level;
                string _name, _email;
                double _cost;
                Console.WriteLine("Enter id, name, email, level, cost");
                _id = Convert.ToInt32(Console.ReadLine());
                _name = Console.ReadLine()!;
                _email = Console.ReadLine()!;
                _level = Convert.ToInt32(Console.ReadLine());
                _cost = Convert.ToInt32(Console.ReadLine());
                Engineer newEngineer = new(_id, _name, _email, (DO.EngineerExperiece)_level, _cost);
                s_dalEngineer!.Update(newEngineer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void deleteEngineer()
        {
            try { 
                Console.WriteLine("Enter engineer's ID");
                int _id = Convert.ToInt32(Console.ReadLine());
                s_dalEngineer!.Delete(_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void crudEngineer()
        {
            string choice;
            choice = Console.ReadLine()!;
            do
            {
                switch (choice)
                {
                    case "1":
                        {
                            createEngineer();
                            break;
                        }
                    case "2":
                        {
                            readEngineer();
                            break;
                        }
                    case "3":
                        {
                            readAllEngineers();
                            break;
                        }
                    case "4":
                        {
                            updateEngineer();
                            break;
                        }
                    case "5":
                        {
                            deleteEngineer();
                            break;
                        }
                    default:
                        break;
                }
                crudMenu("engineer");
                choice = Console.ReadLine()!;
            } while (choice != "0");
        }
        private static int createTask()
        {
            int _complexityLevel;
            int? _engineerId;
            string? _desciption, _alias, _remarks, _deliverable;
            DateTime _deadline, _createdAt;
            Console.WriteLine("Enter description, alias, dead line, deliverable, remarks, engineer ID, complexity level");
            _desciption = Console.ReadLine();
            _alias = Console.ReadLine();
            _createdAt = DateTime.Now;
            _deadline = DateTime.Parse(Console.ReadLine()!);
            _deliverable = Console.ReadLine();
            _remarks = Console.ReadLine();
            _engineerId = Convert.ToInt32(Console.ReadLine());
            _complexityLevel = Convert.ToInt32(Console.ReadLine());
            DO.Task newTask = new(0, _desciption, _alias, false, _createdAt, null, null, _deadline, null, _deliverable, _remarks, _engineerId, (DO.EngineerExperiece)_complexityLevel);
            return s_dalTask!.Create(newTask);
        }
        private static void readTask()
        {
            Console.WriteLine("Enter task's ID");
            int _id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(s_dalTask!.Read(_id));
        }

        private static void readAllTask()
        {
            s_dalTask!.ReadAll().ForEach(ele =>
            {
                Console.WriteLine(ele);
            });
        }

        private static void updateTask()
        {
            try { 
                int _complexityLevel, ID;
                int? _engineerId;
                string? _desciption, _alias, _remarks, _deliverable;
                DateTime _deadline, _createdAt;
                Console.WriteLine("Enter ID, description, alias, dead line, deliverable, remarks, engineer ID, complexity level");
                ID = Convert.ToInt32(Console.ReadLine());
                _desciption = Console.ReadLine();
                _alias = Console.ReadLine();
                _createdAt = DateTime.Now;
                _deadline = DateTime.Parse(Console.ReadLine()!);
                _deliverable = Console.ReadLine();
                _remarks = Console.ReadLine();
                _engineerId = Convert.ToInt32(Console.ReadLine());
                _complexityLevel = Convert.ToInt32(Console.ReadLine());
                DO.Task newTask = new(ID, _desciption, _alias, false, _createdAt, null, null, _deadline, null, _deliverable, _remarks, _engineerId, (DO.EngineerExperiece)_complexityLevel);
                s_dalTask!.Update(newTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void deleteTask()
        {
            try { 
                Console.WriteLine("Enter task's ID");
                int _id = Convert.ToInt32(Console.ReadLine());
                s_dalTask!.Delete(_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void crudTask()
        {
            string choice;
            choice = Console.ReadLine()!;
            do
            {
                switch (choice)
                {
                    case "1":
                        {
                            createTask();
                            break;
                        }
                    case "2":
                        {
                            readTask();
                            break;
                        }
                    case "3":
                        {
                            readAllTask();
                            break;
                        }
                    case "4":
                        {
                            updateTask();
                            break;
                        }
                    case "5":
                        {
                            deleteTask();
                            break;
                        }
                    default:
                        break;
                }
                crudMenu("task");
                choice = Console.ReadLine()!;
            } while (choice != "0");

        }

        private static int createDependency()
        {
            int _dependsOnTask, _dependentTask;
            Console.WriteLine("Enter, pending task, previous task");
            _dependentTask = Convert.ToInt32(Console.ReadLine());
            _dependsOnTask = Convert.ToInt32(Console.ReadLine());
            DO.Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
            return s_dalDependency!.Create(newDependency);
        }
        private static void readDependency()
        {
            Console.WriteLine("Enter dependency's ID");
            int _id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(s_dalDependency!.Read(_id));
        }

        private static void readAllDependencies()
        {
            s_dalDependency!.ReadAll().ForEach(ele =>
            {
                Console.WriteLine(ele);
            });
        }

        private static void updateDependency()
        {
            try { 
                int ID, _dependsOnTask, _dependentTask;
                Console.WriteLine("Enter ID, pending task, previous task");
                ID = Convert.ToInt32(Console.ReadLine());
                _dependentTask = Convert.ToInt32(Console.ReadLine());
                _dependsOnTask = Convert.ToInt32(Console.ReadLine());
                DO.Dependency newDependency = new(ID, _dependentTask, _dependsOnTask);
                s_dalDependency!.Update(newDependency);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void deleteDependency()
        {
            try { 
                Console.WriteLine("Enter dependency's ID");
                int _id = Convert.ToInt32(Console.ReadLine());
                s_dalDependency!.Delete(_id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void crudDependency()
        {
            string choice;
            choice = Console.ReadLine()!;
            do
            {
                switch (choice)
                {
                    case "1":
                        {
                            createDependency();
                            break;
                        }
                    case "2":
                        {
                            readDependency();
                            break;
                        }
                    case "3":
                        {
                            readAllDependencies();
                            break;
                        }
                    case "4":
                        {
                            updateDependency();
                            break;
                        }
                    case "5":
                        {
                            deleteDependency();
                            break;
                        }
                    default:
                        break;
                }
                crudMenu("dependency");
                choice = Console.ReadLine()!;
            } while (choice != "0");
        }
        private static void crudMenu(string entity)
        {    
            Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
            $" 3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n"); 
        }

        private static void mainMenu()
        {
            Console.WriteLine("Choose:\n 0 to exit\n 1 to engineer\n 2 to task\n 3 to dependency\n");
            string choice;
            choice = Console.ReadLine()!;
            do
            {
                switch (choice)
                {
                    case "1":
                        {
                            crudMenu("engineer");
                            crudEngineer();
                            break;
                        }
                    case "2":
                        {
                            crudMenu("task");
                            crudTask();
                            break;
                        }
                    case "3":
                        {
                            crudMenu("dependency");
                            crudDependency();
                            break;
                        }
                }
                Console.WriteLine("Choose:\n 0 to exit\n 1 to engineer\n 2 to task\n 3 to dependency\n");
                choice = Console.ReadLine()!;
            } while (choice != "0");
        }
        static void Main()
        {
            try
            {
                Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependency);
                mainMenu();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

    
