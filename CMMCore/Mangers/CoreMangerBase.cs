namespace Gotchi.Core.Managers;

public class CoreManagerBase
{

    public T ThrowIfModelNotFound<T>(T model, string identifier)
    {
        var checkObject = model ?? throw new ModelNotFoundException<T>(identifier);
        return (T)checkObject;
    }
}
