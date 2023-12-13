
using BIApi;

namespace BlImplementation;

internal class EngineerImplementation :IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || boEngineer.name == "" || boEngineer.cost <= 0)
            //לשאול האם צריך לבדוק תקינות מייל
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.ID, boEngineer.name, boEngineer.email, (DO.EngineerExperiece) boEngineer.level, boEngineer.cost);
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
        return new BO.Engineer()
        {
            ID = id,
            name = doEngineer.name,
            email = doEngineer.email,
            level = (BO.EngineerExperiece)doEngineer.level,
            cost = doEngineer.cost
        };
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                    let boEngineer = new BO.Engineer
                    {
                        ID = doEngineer.ID,
                        name = doEngineer.name,
                        email = doEngineer.email,
                        level = (BO.EngineerExperiece)doEngineer.level,
                        cost = doEngineer.cost
                    }
                    where filter(boEngineer)
                    select boEngineer);
        }
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    ID = doEngineer.ID,
                    name = doEngineer.name,
                    email = doEngineer.email,
                    level = (BO.EngineerExperiece)doEngineer.level,
                    cost = doEngineer.cost
                });
    }

    public void Update(BO.Engineer boEngineer)
    {
        if (boEngineer.ID <= 0 || boEngineer.name == "" || boEngineer.cost <= 0)
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.ID, boEngineer.name, boEngineer.email, (DO.EngineerExperiece)boEngineer.level, boEngineer.cost);
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
}

