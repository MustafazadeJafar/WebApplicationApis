using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Roles;

public class RoleCreateException : BusinessExceptions
{
    public RoleCreateException(string message = "Roles: Something went wrong.") : base(message)
    {
    }
}
