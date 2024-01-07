namespace CSM1.Core.Entities;

public class FileData : BaseEntity
{
    public int BlogId { get; set; }

    // //
    public string Name { get; set; }
    public string ContentType { get; set; }
    public string Path { get; set; }

    // //
    public Blog? Blog { get; set; }
}
