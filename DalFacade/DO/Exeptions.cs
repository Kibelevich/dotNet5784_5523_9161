
namespace DO;
[Serializable]
public class DalDoesNotExistExeption : Exception
{
    public DalDoesNotExistExeption(string? message) : base(message){ }
}

[Serializable]
public class DalAlreadyExistException : Exception
{
    public DalAlreadyExistException(string? message) : base(message) { }
}

[Serializable]
public class DalDeletionImpossibleException : Exception
{
    public DalDeletionImpossibleException(string? message) : base(message) { }

}
