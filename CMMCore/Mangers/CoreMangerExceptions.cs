using CMMCore.Common;

namespace CMMCore.Managers;

public abstract class ManagerCoreException : CoreException
{
    public ManagerCoreException(string message) : base(message) { }
}

public class ModelIsNullCoreException<T> : ManagerCoreException
{
    public ModelIsNullCoreException() 
        : base($"Model is null, Type expected : {typeof(T).FullName}"){}
}

public class ParameterModelIsNullCoreException<T> : ManagerCoreException
{
    public ParameterModelIsNullCoreException()
        : base($"Parameter Model is null, Type expected : {typeof(T).FullName}") { }
}

public class ModelNotFoundCoreException<T> : ManagerCoreException
{
    public ModelNotFoundCoreException(string identifier)
        : base($"Model not found, id : {identifier}, Type expected : {typeof(T).FullName}") { }
}

public class ModelWithIdAlreadyExistsCoreException<T> : ManagerCoreException
{
    public ModelWithIdAlreadyExistsCoreException(string identifier)
        : base($"Model with id : {identifier}, Type {typeof(T).FullName}") { }
}

public class ArgumentStringNullOrEmptyCoreException : ManagerCoreException
{
    public ArgumentStringNullOrEmptyCoreException(string argumentName)
        : base($"Argument \"{argumentName}\" is null or empty") { }
}

//public class CoreArgumentOutOfRangeException : CoreManagerException
//{
//    public CoreArgumentOutOfRangeException(string paramName, string message)
//    {
        
//    }
//}
