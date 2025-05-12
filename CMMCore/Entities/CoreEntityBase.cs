using CMMCore.Repository;

namespace CMMCore.Entities;

public class CoreEntityBase
{
    [CoreId]
    public virtual string? Id { get; set; }
}
