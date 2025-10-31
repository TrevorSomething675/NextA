namespace Nexta.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string toName, string subject, string body, CancellationToken ct = default);
    }
}