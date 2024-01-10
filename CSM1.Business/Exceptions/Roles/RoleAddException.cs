using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Roles;

public class RoleAddException : BusinessExceptions
{
    public RoleAddException(string message = "Roles: Something went wrong.") : base(message)
    {
    }
}
