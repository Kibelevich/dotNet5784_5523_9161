


namespace DalTest;

using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    //private static IEngineer? s_dalEngineer = new EngineerImplementation();
    //private static ITask? s_dalTask = new TaskImplementation();
    //private static IDependency? s_dalDependency = new DependencyImplementation();
    static readonly IDal s_dal = new Dal.DalList();
    /// <summary>
    /// Crud functions for engineer
    /// </summary>
    private static void createEngineer()
    {
        try
        {
            int _id;
            EngineerExperiece _level;
            string _name, _email;
            double _cost;
            Console.WriteLine("Enter id, name, email, level, cost");
            int.TryParse(Console.ReadLine()!, out _id);
            _name = Console.ReadLine()!;
            _email = Console.ReadLine()!;
            EngineerExperiece.TryParse(Console.ReadLine()!, out _level);
            double.TryParse(Console.ReadLine()!, out _cost);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal.Engineer!.Create(newEngineer);
        }
        catch (DalAlreadyExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void readEngineer()
    {
        int _id;
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Engineer!.Read(_id));
    }

    private static void readAllEngineers()
    {
        s_dal.Engineer!.ReadAll(eng => eng.ID > 100)!
              .ToList<Engineer>().ForEach(ele =>
              Console.WriteLine(ele));
    }

    private static void updateEngineer()
    {
        try
        {
            int _id;
            EngineerExperiece _level;
            string _name, _email;
            double _cost;
            Console.WriteLine("Enter id");
            int.TryParse(Console.ReadLine()!, out _id);

            Engineer eng = s_dal.Engineer!.Read(_id) ??
               throw new Exception($"Engineer with ID={_id} not exists");
            Console.WriteLine(eng);
            Console.WriteLine("Enter details to update");
            _name = Console.ReadLine()!;
            _email = Console.ReadLine()!;
            EngineerExperiece.TryParse(Console.ReadLine()!, out _level);
            double.TryParse(Console.ReadLine()!, out _cost);
            if (_name == "") _name = eng.name;
            if (_email == "") _email = eng.email;
            if (_level == 0) _level = eng.level;
            if (_cost == 0) _cost = eng.cost;
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal.Engineer!.Update(newEngineer);

        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

    private static void deleteEngineer()
    {
        try
        {

            Console.WriteLine("Enter engineer's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Engineer!.Delete(_id);
        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    // Submenu for engineer
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

    /// <summary>
    /// Crud functions for task
    /// </summary>
    private static int createTask()
    {
        EngineerExperiece _complexityLevel;
        int _engineerId;
        string _desciption, _alias;
        string? _remarks, _deliverable;
        DateTime _deadline, _createdAt;
        Console.WriteLine("Enter description, alias, dead line, deliverable, remarks, engineer ID, complexity level");
        _desciption = Console.ReadLine()!;
        _alias = Console.ReadLine()!;
        _createdAt = DateTime.Now;
        DateTime.TryParse(Console.ReadLine()!, out _deadline);
        _deliverable = Console.ReadLine();
        _remarks = Console.ReadLine();
        int.TryParse(Console.ReadLine()!, out _engineerId);
        EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
        DO.Task newTask = new(0, _desciption, _alias, false, _createdAt, null, null, _deadline, null, _deliverable, _remarks, _engineerId, _complexityLevel);
        return s_dal.Task!.Create(newTask);
    }
    private static void readTask()
    {
        int _id;
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Task!.Read(_id));
    }

    private static void readAllTask()
    {
        s_dal.Task!.ReadAll(task => task.ID > 50)!
               .ToList<Task>().ForEach(ele =>
                   Console.WriteLine(ele));
    }

    private static void updateTask()
    {
        try
        {
            EngineerExperiece _complexityLevel;
            int ID;
            int _engineerId;
            string? _desciption, _alias, _remarks, _deliverable;
            bool _milestone;
            DateTime _deadline, _createdAt, _complete, _start, _forecastDate;
            Console.WriteLine("Enter ID");
            int.TryParse(Console.ReadLine()!, out ID);
            Task task = s_dal.Task!.Read(ID) ??
               throw new Exception($"Task with ID={ID} not exists");
            Console.WriteLine(task);
            Console.WriteLine("Enter details to update");
            _desciption = Console.ReadLine();
            _alias = Console.ReadLine();
            bool.TryParse(Console.ReadLine()!, out _milestone);
            _createdAt = DateTime.Now;
            DateTime.TryParse(Console.ReadLine()!, out _start);
            DateTime.TryParse(Console.ReadLine()!, out _forecastDate);
            DateTime.TryParse(Console.ReadLine()!, out _deadline);
            DateTime.TryParse(Console.ReadLine()!, out _complete);
            _deliverable = Console.ReadLine();
            _remarks = Console.ReadLine();
            int.TryParse(Console.ReadLine()!, out _engineerId);
            EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
            if (_desciption == "") _desciption = task.desciption!;
            if (_alias == "") _alias = task.alias!;
            if (_start == DateTime.MinValue)
                _start = Convert.ToDateTime(task.start);
            if (_forecastDate == DateTime.MinValue)
                _forecastDate = Convert.ToDateTime(task.forecastDate);
            if (_deadline == DateTime.MinValue)
                _deadline = task.deadline;
            if (_complete == DateTime.MinValue)
                _complete = Convert.ToDateTime(task.complete);
            if (_deliverable == "") _deliverable = task.deliverable!;
            if (_remarks == "") _remarks = task.remarks!;
            if (_engineerId == 0) _engineerId = Convert.ToInt32(task.engineerId);
            if (_complexityLevel == 0) _complexityLevel = (EngineerExperiece)task.complexityLevel!;
            Task newTask = new(ID, _desciption!, _alias!, _milestone, _createdAt, _start, _forecastDate, _deadline, _complete, _deliverable, _remarks, _engineerId, _complexityLevel);
            s_dal.Task!.Update(newTask);
        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void deleteTask()
    {
        try
        {
            Console.WriteLine("Enter task's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Task!.Delete(_id);
        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Submenu for task
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

    /// <summary>
    /// Crud functions for dependency
    /// </summary>
    private static int createDependency()
    {
        int _dependsOnTask, _dependentTask;
        Console.WriteLine("Enter, pending task, previous task");
        int.TryParse(Console.ReadLine()!, out _dependentTask);
        int.TryParse(Console.ReadLine()!, out _dependsOnTask);
        Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
        return s_dal.Dependency!.Create(newDependency);
    }
    private static void readDependency()
    {
        int _id;
        Console.WriteLine("Enter dependency's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Dependency!.Read(_id));
    }

    private static void readAllDependencies()
    {
        s_dal.Dependency!.ReadAll(dep => dep.ID > 100)!
                .ToList<Dependency>().ForEach(ele =>
                Console.WriteLine(ele));
    }

    private static void updateDependency()
    {
        try
        {
            int ID, _dependsOnTask, _dependentTask;
            Console.WriteLine("Enter ID");
            int.TryParse(Console.ReadLine()!, out ID);
            Dependency depend = s_dal.Dependency!.Read(ID) ??
               throw new Exception($"Dependency with ID={ID} not exists");
            Console.WriteLine(s_dal.Dependency!.Read(ID));
            int.TryParse(Console.ReadLine()!, out _dependentTask);
            int.TryParse(Console.ReadLine()!, out _dependsOnTask);
            if (_dependentTask == 0) _dependentTask = depend.dependentTask;
            if (_dependsOnTask == 0) _dependsOnTask = depend.dependsOnTask;
            Dependency newDependency = new(ID, _dependentTask, _dependsOnTask);
            s_dal.Dependency!.Update(newDependency);
        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void deleteDependency()
    {
        try
        {
            Console.WriteLine("Enter dependency's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Dependency!.Delete(_id);
        }
        catch (DalDoesNotExistExeption e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Submenu for dependency
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

    // Prints the entity's options 
    private static void crudMenu(string entity)
    {
        Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
        $" 3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n");
    }

    // Main menu - entity selection 
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
            Initialization.Do(s_dal);
            mainMenu();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
        }
        catch (DalAlreadyExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}



