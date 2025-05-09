using Nexta.Application.Commands.RegistrationCommand;
using Nexta.Application.Commands.LoginCommand;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login(LoginCommandRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register(RegistrationCommandRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
	}
}