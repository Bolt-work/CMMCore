namespace Gotchi.Core.Managers;

public abstract class CoreManagerException : Exception 
{
    public CoreManagerException(string message) : base(message) { }
}

public class ModelIsNullException<T> : CoreManagerException
{
    public ModelIsNullException() 
        : base($"Model is null, Type expected : {typeof(T).FullName}"){}
}

public class ParameterModelIsNullException<T> : CoreManagerException
{
    public ParameterModelIsNullException()
        : base($"Parameter Model is null, Type expected : {typeof(T).FullName}") { }
}

public class ModelNotFoundException<T> : CoreManagerException
{
    public ModelNotFoundException(string identifier)
        : base($"Model not found, id : {identifier}, Type expected : {typeof(T).FullName}") { }
}

public class ModelWithIdAlreadyExistsException<T> : CoreManagerException
{
    public ModelWithIdAlreadyExistsException(string identifier)
        : base($"Model with id : {identifier}, Type {typeof(T).FullName}") { }
}

public class ArgumentStringNullOrEmptyException : CoreManagerException
{
    public ArgumentStringNullOrEmptyException(string argumentName)
        : base($"Argument \"{argumentName}\" is null or empty") { }
}
