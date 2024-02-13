
using BlApi;

namespace BlImplementation;

/// <summary>
/// The implementation of engineerInList's CRUD methods in BL
/// </summary>

internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Reads all entity objects
    /// </summary>
    /// <param name="filter">A boolean function that is a condition for returning a value</param>
    /// <returns>Collection of all objects</returns>
    public IEnumerable<BO.EngineerInList?> ReadAll(Func<BO.EngineerInList, bool>? filter = null)
    {
        if (filter != null)
        {
            return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                   let boEngineerInList = new BO.EngineerInList()
                   {
                       ID = doEngineer.ID,
                       name = doEngineer.Name,
                       level = (BO.EngineerExperiece) doEngineer.Level
                   }
                   where filter(boEngineerInList)
                   select boEngineerInList;
        }
        return from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
               select new BO.EngineerInList()
               {
                   ID = doEngineer.ID,
                   name = doEngineer.Name,
                   level = (BO.EngineerExperiece)doEngineer.Level
               };
    }
}
