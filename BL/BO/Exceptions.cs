
namespace BO;


/// <summary>
/// BL exceptions
/// </summary>



[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

[Serializable]
public class BlAlreadyExistException : Exception
{
    public BlAlreadyExistException(string? message) : base(message) { }
    public BlAlreadyExistException(string message, Exception innerException)
                : base(message, innerException) { }
}


[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}
[Serializable]
public class BlIllegalPropertyException : Exception
{
    public BlIllegalPropertyException(string? message) : base(message) { }
}
[Serializable]

public class BlDeletionImpossibleException : Exception
{
    public BlDeletionImpossibleException(string? message) : base(message) { }
}
[Serializable]

public class FormatException : Exception
{
    public FormatException(string? message) : base(message) { }
}
[Serializable]
public class BlDeadlinePassedException: Exception {
    public BlDeadlinePassedException(string? message): base(message) { }
}

