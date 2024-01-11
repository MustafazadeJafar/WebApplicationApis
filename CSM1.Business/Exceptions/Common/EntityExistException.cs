using CSM1.Core.Entities.Common;

namespace CSM1.Business.Exceptions.Common;

public class EntityExistException<T> : BusinessExceptions where T : BaseEntity
{
    public EntityExistException() : base(typeof(T).Name + " not found")
    {
    }

    public EntityExistException(string? message) : base(message)
    {
    }
}
