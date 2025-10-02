using Nexta.Domain.Abstractions.Services;
using Microsoft.Extensions.Options;
using Nexta.Domain.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace Nexta.Infrastructure.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailOptions _options;
		public EmailService(IOptions<EmailOptions> options)
		{
			_options = options.Value;
		}

		public async Task SendEmailAsync(string toEmail, string toName, string subject, string body, CancellationToken ct = default)
		{
			try
			{
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
				message.To.Add(new MailboxAddress(toName ?? string.Empty, toEmail));
				message.Subject = subject;
				message.Body = new TextPart("html")
				{
					Text = body
				};

				using (var client = new SmtpClient())
				{
					await client.ConnectAsync(
						_options.SmtpServer,
						_options.Port,
						_options.EnableSsl ? MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.None,
						ct); 

					await client.AuthenticateAsync(_options.Username, _options.Password, ct);
					await client.SendAsync(message, ct);
					await client.DisconnectAsync(true, ct);
				}
			}
			catch (Exception ex)
			{
				var errorMessage = ex.Message;
			} // Мы брошены
		}
	}
}