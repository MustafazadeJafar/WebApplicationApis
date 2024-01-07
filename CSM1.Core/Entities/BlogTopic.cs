namespace CSM1.Core.Entities;

public class BlogTopic : BaseEntity
{
    public int BlogId { get; set; }
    public int TopicId { get; set; }

    // Reletaion //
    public Blog? Blog { get; set; }
    public Topic? Topic { get; set; }
}
