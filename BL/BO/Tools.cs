
using System.Reflection;

namespace BO;

public static class Tools
{
    public static string ToStringProperty<T>(T item)
    {
        string result = item!.GetType().ToString() + ": ";
        foreach (PropertyInfo prop in item.GetType().GetProperties())
        {
            result += prop.Name;
            result += " ";
            result += item.GetType().GetProperty(prop.Name)?.GetValue(item);
            result += "\n";
        }
        return result;
    }

}
