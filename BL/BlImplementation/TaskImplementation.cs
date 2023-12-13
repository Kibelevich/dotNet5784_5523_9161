
using BIApi;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private IDal _dal = Factory.Get;

    public void Create(BO.Task boTask)
    {
        if (boTask.alias == "" )
            //לשאול האם צריך לבדוק תקינות מזהה
            throw new BO.BlIllegalPropertyException($"Illegal property");
        DO.Engineer doEngineer = new DO.Engineer
        (boEngineer.ID, boEngineer.name, boEngineer.email, (DO.EngineerExperiece)boEngineer.level, boEngineer.cost);
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
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return (from DO.Task doTask in _dal.Task.ReadAll()
                    let boTask = new BO.Task
                    {
                        ID = doTask.ID,
                        desciption = doTask.desciption,
                        alias = doTask.alias,

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

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
