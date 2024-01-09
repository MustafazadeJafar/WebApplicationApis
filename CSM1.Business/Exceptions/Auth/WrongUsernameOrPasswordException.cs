using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Auth;

public class WrongUsernameOrPasswordException : BusinessExceptions
{
    public WrongUsernameOrPasswordException(string message = "Auth: Wrong username or password.") : base(message)
    {
    }
}
