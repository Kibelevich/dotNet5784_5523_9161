
using BlApi;

namespace BlImplementation;

internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public IEnumerable<BO.EngineerInList?> ReadAll(Func<BO.EngineerInList, bool>? filter = null)
    {
        if (filter != null)
        {
            return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                   let boEngineerInList = new BO.EngineerInList()
                   {
                       ID = doEngineer.ID,
                       name = doEngineer.name,
                       level = (BO.EngineerExperiece) doEngineer.level
                   }
                   where filter(boEngineerInList)
                   select boEngineerInList;
        }
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
               select new BO.EngineerInList()
               {
                   ID = doEngineer.ID,
                   name = doEngineer.name,
                   level = (BO.EngineerExperiece)doEngineer.level
               };
    }
}
