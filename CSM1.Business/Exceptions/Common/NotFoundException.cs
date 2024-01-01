using CSM1.Core.Entities.Common;

namespace CSM1.Business.Exceptions.Common;

public class NotFoundException<T> : Exception where T : BaseEntity
{
    public NotFoundException() : base(typeof(T).Name + " not found")
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}
