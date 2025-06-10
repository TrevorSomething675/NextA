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
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Login(LoginCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(RegistrationCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Register(RegistrationCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}