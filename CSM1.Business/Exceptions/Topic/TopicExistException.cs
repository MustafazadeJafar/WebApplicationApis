using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Topic;

public class TopicExistException : BusinessExceptions
{
    public TopicExistException() : base("Topic already added")
    {
    }

    public TopicExistException(string? message) : base(message)
    {
    }
}
