namespace Nexta.Domain.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string toName, string subject, string body, CancellationToken ct = default);
    }
}