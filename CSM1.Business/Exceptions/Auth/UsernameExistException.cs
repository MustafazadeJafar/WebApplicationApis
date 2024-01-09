using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Auth;

public class UsernameExistException : BusinessExceptions
{
    public UsernameExistException(string message = "Auth: Username already registered.") : base(message)
    {
    }
}
