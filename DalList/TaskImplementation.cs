
namespace Dal;
using DO;
using DalApi;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextIdT;
        Task task = item with { ID = id };
        DataSource.Tasks.Add(task);
        return id;
    }

    public void Delete(int id)
    {
        Task? task = DataSource.Tasks.Find(ele => ele.ID == id);
        if (task == null)
            throw new Exception($"Task with ID={id} not exists");
        DataSource.Tasks.Remove(task);
        task = task with { complete = DateTime.Now };
        DataSource.Tasks.Add(task);
    }

    public Task? Read(int id)
    {
        Task? task = null;
        DataSource.Tasks.ForEach(element=>
        {
            if (element.ID == id)
                task = element;
        });
        return task;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        Task task = DataSource.Tasks.Find(ele => ele.ID == item.ID)!;
        if (task == null)
            throw new Exception($"Task with ID={item.ID} not exists");
        DataSource.Tasks.Remove(task);
        DataSource.Tasks.Add(item);
    }
}
