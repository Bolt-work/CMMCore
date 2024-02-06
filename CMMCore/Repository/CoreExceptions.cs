using CMMCore.Common;

namespace CMMCore.Repository
{
    public abstract class CoreRepositoryException : CoreException 
    {
        public CoreRepositoryException(string message) : base(message) { }
    }
    public class CoreIdAttributeNotFoundException : CoreRepositoryException
    {
        public CoreIdAttributeNotFoundException(object obj)
            : base(
                 $"Object of type : {obj.GetType()}. Has no CoreId Attribute"
                 )
        { }
    }
}
