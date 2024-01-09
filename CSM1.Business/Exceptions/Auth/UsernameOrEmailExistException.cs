using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Auth;

public class UsernameOrEmailExistException : BusinessExceptions
{
    public UsernameOrEmailExistException(string message = "Auth: Username already registered.") : base(message)
    {
    }
}
