namespace CMMFileDatabase.FileDatabase;

public abstract class FileModelBase
{
    public Guid? Id { get; set; }
    public DateTime? Created { get; set; }
    public DateTime? LastModified { get; set; }

    protected FileModelBase()
    {
        Created = DateTime.UtcNow;
    }
}
