

namespace DalTest;
using DalApi;
using DO;

/// <summary>
/// The main program for checking the DAL
/// </summary>
internal class Program
{
    static readonly IDal s_dal = Factory.Get;
    /// <summary>
    /// Crud functions for engineer
    /// </summary>
    private static void CreateEngineer()
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
            EngineerExperiece.TryParse(Console.ReadLine(), out _level);
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

    private static void ReadEngineer()
    {
        int _id;
        Console.WriteLine("Enter engineer's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Engineer!.Read(_id));
    }

    private static void ReadAllEngineers()
    {
        s_dal.Engineer!.ReadAll(eng => eng.ID > 0)!
              .ToList<Engineer>().ForEach(ele =>
              Console.WriteLine(ele));
    }

    private static void UpdateEngineer()
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
            EngineerExperiece.TryParse(Console.ReadLine(), out _level);
            double.TryParse(Console.ReadLine()!, out _cost);
            if (_name == "") _name = eng.Name;
            if (_email == "") _email = eng.Email;
            if (_level == 0) _level = eng.Level;
            if (_cost == 0) _cost = Convert.ToDouble(eng.Cost);
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal.Engineer!.Update(newEngineer);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void DeleteEngineer()
    {
        try
        {

            Console.WriteLine("Enter engineer's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Engineer!.Delete(_id);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Submenu for engineer
    private static void CrudEngineer()
    {
        string choice;
        choice = Console.ReadLine()!;
        do
        {
            switch (choice)
            {
                case "1":
                    {
                        CreateEngineer();
                        break;
                    }
                case "2":
                    {
                        ReadEngineer();
                        break;
                    }
                case "3":
                    {
                        ReadAllEngineers();
                        break;
                    }
                case "4":
                    {
                        UpdateEngineer();
                        break;
                    }
                case "5":
                    {
                        DeleteEngineer();
                        break;
                    }
                default:
                    break;
            }
            CrudMenu("engineer");
            choice = Console.ReadLine()!;
        } while (choice != "0");
    }

    /// <summary>
    /// Crud functions for task
    /// </summary>
    private static int CreateTask()
    {
        EngineerExperiece _complexityLevel;
        int _engineerId;
        string _description, _alias;
        string? _remarks, _deliverable;
        DateTime _deadline, _createdAt;
        TimeSpan _requiredEffortTime=TimeSpan.Zero;
        Console.WriteLine("Enter description, alias, required effort time, dead line, deliverable, remarks, engineer ID, complexity level");
        _description = Console.ReadLine()!;
        _alias = Console.ReadLine()!;
        TimeSpan.TryParse(Console.ReadLine()!, out _requiredEffortTime);
        _createdAt = DateTime.Now;
        DateTime.TryParse(Console.ReadLine()!, out _deadline);
        _deliverable = Console.ReadLine();
        _remarks = Console.ReadLine();
        int.TryParse(Console.ReadLine()!, out _engineerId);
        EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
        Task newTask = new(0, _description, _alias, false, _requiredEffortTime, _createdAt, null, null, null,
            _deadline, null, _deliverable, _remarks, _engineerId, _complexityLevel);
        return s_dal.Task!.Create(newTask);
    }
    private static void ReadTask()
    {
        int _id;
        Console.WriteLine("Enter task's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Task!.Read(_id));
    }

    private static void ReadAllTask()
    {
        s_dal.Task!.ReadAll(task => task.ID > 50)!
               .ToList<DO.Task>().ForEach(ele =>
                   Console.WriteLine(ele));
    }

    private static void UpdateTask()
    {
        try
        {
            EngineerExperiece _complexityLevel;
            int ID, _engineerId;
            string? _description, _alias, _remarks, _deliverable;
            bool _milestone;
            DateTime _deadline, _createdAt, _complete, _start, _forecastEndDate, _baselineStart;
            TimeSpan _requiredEffortTime = TimeSpan.Zero;
            Console.WriteLine("Enter ID");
            int.TryParse(Console.ReadLine()!, out ID);
            Task task = s_dal.Task!.Read(ID) ??
               throw new Exception($"Task with ID={ID} not exists");
            Console.WriteLine(task);
            Console.WriteLine("Enter details to update");
            _description = Console.ReadLine();
            _alias = Console.ReadLine();
            bool.TryParse(Console.ReadLine()!, out _milestone);
            TimeSpan.TryParse(Console.ReadLine()!, out _requiredEffortTime);
            _createdAt = DateTime.Now;
            DateTime.TryParse(Console.ReadLine()!, out _baselineStart);
            DateTime.TryParse(Console.ReadLine()!, out _start);
            DateTime.TryParse(Console.ReadLine()!, out _forecastEndDate);
            DateTime.TryParse(Console.ReadLine()!, out _deadline);
            DateTime.TryParse(Console.ReadLine()!, out _complete);
            _deliverable = Console.ReadLine();
            _remarks = Console.ReadLine();
            int.TryParse(Console.ReadLine()!, out _engineerId);
            EngineerExperiece.TryParse(Console.ReadLine()!, out _complexityLevel);
            if (_description == "") _description = task.Description!;
            if (_alias == "") _alias = task.Alias!;
            //if (_requiredEffortTime == TimeSpan.Zero)
            //    _requiredEffortTime = task.requiredEffortTime;
            if (_baselineStart == DateTime.MinValue)
                _baselineStart = Convert.ToDateTime(task.BaselineStart);
            if (_start == DateTime.MinValue)
                _start = Convert.ToDateTime(task.Start);
            if (_forecastEndDate == DateTime.MinValue)
                _forecastEndDate = Convert.ToDateTime(task.ForecastEndDate);
            if (_deadline == DateTime.MinValue)
                _deadline = task.Deadline;
            if (_complete == DateTime.MinValue)
                _complete = Convert.ToDateTime(task.Complete);
            if (_deliverable == "") _deliverable = task.Deliverable!;
            if (_remarks == "") _remarks = task.Remarks!;
            if (_engineerId == 0) _engineerId = Convert.ToInt32(task.EngineerId);
            if (_complexityLevel == 0) _complexityLevel = (EngineerExperiece)task.ComplexityLevel!;
            Task newTask = new(ID, _description!, _alias!, _milestone,_requiredEffortTime, _createdAt,_baselineStart,
                _start, _forecastEndDate, _deadline, _complete, _deliverable, _remarks, _engineerId, _complexityLevel);
            s_dal.Task!.Update(newTask);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void DeleteTask()
    {
        try
        {
            Console.WriteLine("Enter task's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Task!.Delete(_id);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

        }
    }
    // Submenu for task
    private static void CrudTask()
    {
        string choice;
        choice = Console.ReadLine()!;
        do
        {
            switch (choice)
            {
                case "1":
                    {
                        CreateTask();
                        break;
                    }
                case "2":
                    {
                        ReadTask();
                        break;
                    }
                case "3":
                    {
                        ReadAllTask();
                        break;
                    }
                case "4":
                    {
                        UpdateTask();
                        break;
                    }
                case "5":
                    {
                        DeleteTask();
                        break;
                    }
                default:
                    break;
            }
            CrudMenu("task");
            choice = Console.ReadLine()!;
        } while (choice != "0");
    }

    /// <summary>
    /// Crud functions for dependency
    /// </summary>
    private static int CreateDependency()
    {
        int _dependsOnTask, _dependentTask;
        Console.WriteLine("Enter, pending task, previous task");
        int.TryParse(Console.ReadLine(), out _dependentTask);
        int.TryParse(Console.ReadLine(), out _dependsOnTask);
        Dependency newDependency = new(0, _dependentTask, _dependsOnTask);
        return s_dal.Dependency!.Create(newDependency);
    }
    private static void ReadDependency()
    {
        int _id;
        Console.WriteLine("Enter dependency's ID");
        int.TryParse(Console.ReadLine()!, out _id);
        Console.WriteLine(s_dal.Dependency!.Read( d=>d.ID==_id));
    }

    private static void ReadAllDependencies()
    {
        s_dal.Dependency!.ReadAll(dep => dep.ID > 100)!
                .ToList<Dependency>().ForEach(ele =>
                Console.WriteLine(ele));
    }

    private static void UpdateDependency()
    {
        try
        {
            int ID;
            int _dependsOnTask, _dependentTask;
            Console.WriteLine("Enter ID");
            int.TryParse(Console.ReadLine()!, out ID);
            Dependency depend = s_dal.Dependency.Read(ID) ??
               throw new Exception($"Dependency with ID={ID} not exists");
            Console.WriteLine(s_dal.Dependency!.Read(ID));
            int.TryParse(Console.ReadLine()!, out _dependentTask);
            int.TryParse(Console.ReadLine()!, out _dependsOnTask);
            if (_dependentTask == 0) _dependentTask = Convert.ToInt32(depend.DependentTask);
            if (_dependsOnTask == 0) _dependsOnTask = Convert.ToInt32(depend.DependsOnTask);
            Dependency newDependency = new(ID, _dependentTask, _dependsOnTask);
            s_dal.Dependency!.Update(newDependency);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void DeleteDependency()
    {
        try
        {
            Console.WriteLine("Enter dependency's ID");
            int _id;
            int.TryParse(Console.ReadLine()!, out _id);
            s_dal.Dependency!.Delete(_id);
        }
        catch (DalDoesNotExistException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    // Submenu for dependency
    private static void CrudDependency()
    {
        string choice;
        choice = Console.ReadLine()!;
        do
        {
            switch (choice)
            {
                case "1":
                    {
                        CreateDependency();
                        break;
                    }
                case "2":
                    {
                        ReadDependency();
                        break;
                    }
                case "3":
                    {
                        ReadAllDependencies();
                        break;
                    }
                case "4":
                    {
                        UpdateDependency();
                        break;
                    }
                case "5":
                    {
                        DeleteDependency();
                        break;
                    }
                default:
                    break;
            }
            CrudMenu("dependency");
            choice = Console.ReadLine()!;
        } while (choice != "0");
    }

    // Prints the entity's options 
    private static void CrudMenu(string entity)
    {
        Console.WriteLine($"Choose:\n 0 to exit\n 1 to create a new {entity}\n 2 to read the {entity}\n" +
        $" 3 to read all\n 4 to update the {entity}\n 5 to delete the {entity}\n");
    }

    // Main menu - entity selection 
    private static void MainMenu()
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
                        CrudMenu("engineer");
                        CrudEngineer();
                        break;
                    }
                case "2":
                    {
                        CrudMenu("task");
                        CrudTask();
                        break;
                    }
                case "3":
                    {
                        CrudMenu("dependency");
                        CrudDependency();
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
            Initialization.Do();
            MainMenu();
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



