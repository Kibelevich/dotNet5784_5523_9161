using System.Text.RegularExpressions;
using BlApi;

namespace BlImplementation;

internal class EngineerImplementation :IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || boEngineer.name == "" || 
            boEngineer.cost <= 0||Regex.IsMatch(boEngineer.email,@"(@)(.+)$"))
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = replaceBoToDo(boEngineer);
        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
        }
        catch (DO.DalAlreadyExistException ex)
        {
            throw new BO.BlAlreadyExistException($"Engineer with ID={boEngineer.ID} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
        IEnumerable<DO.Task?> tasks = _dal.Task.ReadAll((t) => t.engineerId == id);
        if (tasks == null)
            try
            {
                _dal.Engineer.Delete(id);
            }
            catch (DO.DalAlreadyExistException ex)
            {
                throw new BO.BlAlreadyExistException($"Engineer with ID={id} already exists", ex);
            }
        else throw new BO.BlDeletionImpossibleException($"Impossible to delete this engineer");
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist");
        return replaceDoToBo(doEngineer);
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    let boEngineer = replaceDoToBo(doEngineer)
                    where filter(boEngineer)
                    select boEngineer);
        }
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select replaceDoToBo(doEngineer));
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || boEngineer.name == "" || boEngineer.cost <= 0)
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = replaceBoToDo(boEngineer);
        try
        {
            _dal.Engineer.Delete(boEngineer.ID);
            int id = _dal.Engineer.Create(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.ID} does not exists", ex);
        }
    }
    BO.Engineer replaceDoToBo(DO.Engineer doEngineer)
    {
        DO.Task? doCTask = _dal.Task.Read(task => task.engineerId == doEngineer.ID);
        BO.TaskInEngineer? boCTask = null;
        if (doCTask != null)
            boCTask = new BO.TaskInEngineer() { ID = doCTask.ID, alias = doCTask.alias };
        return new BO.Engineer()
        {
            ID = doEngineer.ID,
            name = doEngineer.name,
            email = doEngineer.email,
            level = (BO.EngineerExperiece)doEngineer.level,
            cost = doEngineer.cost,
            currentTask = boCTask
        };
    }
    DO.Engineer replaceBoToDo(BO.Engineer boEngineer)
    {
        return new DO.Engineer
        (boEngineer.ID, boEngineer.name, boEngineer.email, (DO.EngineerExperiece)boEngineer.level, boEngineer.cost);
    }

}

