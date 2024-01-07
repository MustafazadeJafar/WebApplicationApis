using CSM1.Core.Entities.Common;

namespace CSM1.Core.Entities;

public class Topic : BaseEntity
{
    public string Name { get; set; }

    // Reletaion //
    public IEnumerable<BlogTopic>? BlogTopics { get; set; }
}
