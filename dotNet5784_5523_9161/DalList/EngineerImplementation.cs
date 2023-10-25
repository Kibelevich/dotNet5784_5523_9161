
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
        Engineer engineer = DataSource.Engineers.Find(ele => ele.ID == id)!;
        if (engineer == null)
            throw new Exception($"Engineer with ID={id} not exists");
        DataSource.Engineers.Remove(engineer);
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
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer engineer = DataSource.Engineers.Find(ele => ele.ID == item.ID)!;
        if (engineer == null)
            throw new Exception($"Engineer with ID={item.ID} not exists");
        DataSource.Engineers.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}
