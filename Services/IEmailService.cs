namespace AIAssistantMacos.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string name, string activationCode);
}