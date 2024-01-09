namespace CSM1.Business.Exceptions.Common;

public abstract class BusinessExceptions : Exception
{
    public BusinessExceptions(string message) : base(message) { }
}
