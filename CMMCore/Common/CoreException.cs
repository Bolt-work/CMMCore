namespace CMMCore.Common;

public abstract class CoreException : Exception
{
    public CoreException(string message) : base(message) { }
}
