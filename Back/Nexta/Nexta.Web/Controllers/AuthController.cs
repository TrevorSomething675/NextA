using Nexta.Application.Commands.AuthCommands.RegistrationCommand;
using Nexta.Application.Commands.AuthCommands.LoginCommand;
using Nexta.Application.Queries.AuthQueries.CheckAuthQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nexta.Application.Queries.AuthQueries.CheckRegisterUserQuery;

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
		public async Task<IResult> Login([FromForm] LoginCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(RegistrationCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Register([FromForm] RegistrationCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(CheckRegisterUserQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CheckRegisterUser([FromBody] CheckRegisterUserQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[Authorize]
		[HttpPost("[action]")]
		[ProducesResponseType(typeof(CheckAuthQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CheckAuth([FromBody] CheckAuthQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}