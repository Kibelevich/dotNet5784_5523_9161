
namespace DO;
[Serializable]
public class DalDoesNotExistExeption : Exception
{
    public DalDoesNotExistExeption(string? message) : base(message){ }
}

public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? message) : base(message) { }
}

public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }

}
