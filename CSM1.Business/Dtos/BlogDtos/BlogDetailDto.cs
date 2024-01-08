using CSM1.Business.Dtos.TopicDtos;
using CSM1.Core.Entities;

namespace CSM1.Business.Dtos.BlogDtos;

public class BlogDetailDto
{
    public int Id { get; set; }
    public virtual string UserName { get; set; }

    // //
    public virtual string TextContent { get; set; }
    public virtual DateTime LastUpdatedTime { get; set; }
    public virtual int UpdatedTimes { get; set; } = 0;

    // //
    //public virtual IEnumerable<FileData>? FileDatas { get; set; }
    public virtual IEnumerable<TopicDetailDto>? Topics { get; set; }
}
