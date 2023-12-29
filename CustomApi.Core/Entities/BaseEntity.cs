namespace CustomApi.Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public virtual DateTime? CreatedAt { get; set; } = DateTime.Now;
    public virtual bool? IsActive { get; set; }
}
