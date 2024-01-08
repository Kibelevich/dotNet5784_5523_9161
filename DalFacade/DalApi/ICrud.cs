

namespace DalApi;
/// <summary>
/// A general interface to define the CRUD methods  in DAL
/// </summary>
/// <typeparam name="T">The entity type </typeparam>
public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object
    T? Read(int id); //Reads entity object by its ID 
    T? Read(Func<T, bool> filter); // Reads entity object by filter
    IEnumerable<T?> ReadAll(Func<T, bool>? filter = null); //Reads all entity objects by condition
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
