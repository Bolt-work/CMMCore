namespace CMMCore.Managers;

public class CoreManagerBase
{

    public T ThrowIfModelNotFound<T>(T model, string identifier)
    {
        var checkObject = model ?? throw new ModelNotFoundCoreException<T>(identifier);
        return (T)checkObject;
    }
}
