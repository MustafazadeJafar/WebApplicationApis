namespace CustomApi.Business.ExternalServices.Interfaces;

public interface IEmailService
{
    public Task SendEmail(string email);
}
