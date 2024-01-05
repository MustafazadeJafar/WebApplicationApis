namespace CSM1.Business.ExternalServices.Interfaces;

public interface IEmailService
{
    public bool Send(string mail, string header, string body, bool isHtml);
}
