
using System.Reflection;

namespace BO;


/// <summary>
/// Class for help
/// </summary>
public static class Tools
{
    /// <summary>
    /// General function that convert an object to string
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="item">The object to convert</param>
    /// <returns>The object converted to a string representation</returns>
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
