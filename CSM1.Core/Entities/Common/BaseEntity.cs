namespace CSM1.Core.Entities.Common;

public class BaseEntity
{
    public int Id { get; set; }

    // //
    public virtual DateTime CreatedTime { get; set; }
    public virtual bool IsActive { get; set; } = true;
}
