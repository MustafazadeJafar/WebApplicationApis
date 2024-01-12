namespace CSM1.Business.Dtos.CommentDtos;

public class CommentListItemDto
{
    public int Id { get; set; }
    public int ParentId { get; set; }

    // //
    public string Content { get; set; }

    // //
    public DateTime CreatedTime { get; set; }
}
