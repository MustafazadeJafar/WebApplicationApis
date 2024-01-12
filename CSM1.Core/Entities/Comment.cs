namespace CSM1.Core.Entities;

public class Comment : BaseEntity
{
    public string AppUserId { get; set; }
    public int BlogId { get; set; }
    public int? ParentId { get; set; }

    // //
    public string Content { get; set; }

    // //
    public AppUser? AppUser { get; set; }
    public Blog? Blog { get; set; }
    public Comment? Parent { get; set; }
    public IEnumerable<Comment>? Childs { get; set; }
}
