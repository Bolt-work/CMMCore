namespace CMMCore.Repository
{
    public class CoreIdAttributeNotFoundException : Exception
    {
        public CoreIdAttributeNotFoundException(object obj)
            : base(
                 $"Object of type : {obj.GetType()}. Has no CoreId Attribute"
                 )
        { }
    }
}
