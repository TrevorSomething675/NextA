using Microsoft.AspNetCore.Mvc;
using Nexta.Domain.Abstractions.Services;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
    public class MailController : Controller
    {
		private readonly IEmailService _emailService;
		public MailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> SendTestEmail([FromHeader] string email)
		{
			var subject = "Тестовое письмо";
			var body = "<h1>Это тестовое письмо</h1><p>Отправлено через MailKit</p>";

			await _emailService.SendEmailAsync(email, "defaultName", subject, body);

			return Ok();
		}
	}
}
