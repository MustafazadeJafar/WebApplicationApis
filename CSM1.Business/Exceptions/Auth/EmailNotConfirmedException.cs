using CSM1.Business.Exceptions.Common;

namespace CSM1.Business.Exceptions.Auth;

internal class EmailNotConfirmedException : BusinessExceptions
{
    public EmailNotConfirmedException(string message = "Auth: Email is not confirmed") : base(message)
    {
    }
}
