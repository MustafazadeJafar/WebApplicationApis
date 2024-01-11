namespace CSM1.Core.Entities;

public class Blog : BaseEntity
{
    public string AppUserId { get; set; }

    // //
    public string TextContent { get; set; }
    public DateTime LastUpdatedTime { get; set; }
    public int UpdatedTimes { get; set; } = 0;

    // Reletaion //
    public AppUser? AppUser { get; set; }
    public IEnumerable<FileData>? FileDatas { get; set; }
    public IEnumerable<BlogTopic>? BlogTopics { get; set; }
}
