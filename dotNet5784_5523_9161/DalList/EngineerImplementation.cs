
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == item.ID)
                throw new Exception($"Engineer with ID={item.ID} already exists");
        });
        DataSource.Engineers.Add(item);
        return item.ID;
    }
    public void Delete(int id)
    {
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == id)
            {
                DataSource.Engineers.Remove(engineer);
                return;
            }
        });
        throw new Exception($"Engineer with ID={id} not exists");
    }

    public Engineer? Read(int id)
    {
        Engineer? eng = null;
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == id)
                eng = engineer;
        });
        return eng;
    }

    public List<Engineer> ReadAll()
    {
        //////////////////////////////////////////////////////////////////// אם זה נקרא עותק
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        DataSource.Engineers.ForEach(engineer =>
        {
            if (engineer.ID == item.ID)
            {
                DataSource.Engineers.Remove(engineer);
                DataSource.Engineers.Add(item);
                return;
            }
        });
        throw new Exception($"Engineer with ID={item.ID} not exists");
    }
}
