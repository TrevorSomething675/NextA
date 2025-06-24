using Nexta.Application.Commands.Auth.RegistrationCommand;
using Nexta.Application.Queries.Auth.IsAuthenticatedQuery;
using Nexta.Application.Queries.Auth.IsRegisteredQuery;
using Nexta.Application.Commands.Auth.LoginCommand;
using Microsoft.AspNetCore.Authorization;
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
		[ProducesResponseType(typeof(IsRegisteredQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> IsRegisterUser([FromBody] IsRegisteredQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[Authorize]
		[HttpPost("[action]")]
		[ProducesResponseType(typeof(IsAuthenticatedQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> IsAuth([FromBody] IsAuthenticatedQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}