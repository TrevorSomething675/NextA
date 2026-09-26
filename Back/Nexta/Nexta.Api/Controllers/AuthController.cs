using Nexta.Application.Queries.Auth.IsRegisteredQuery;
using Nexta.Application.Commands.Auth.LoginCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Nexta.Web.Models.Auth;
using Nexta.Application.Commands.Auth.RegisterCommand;
using Nexta.Application.Commands.Auth.CheckAuthCommand;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public AuthController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Login([FromBody] LoginRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<LoginCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(RegisterCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Register([FromBody] RegistrationRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<RegisterCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

        [HttpGet("[action]")]
		[ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
		public async Task<IResult> IsRegisterUser([FromQuery] string email, CancellationToken ct = default)
		{
			var query = new IsRegisteredQuery(email);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[Authorize]
		[HttpPost("[action]")]
		[ProducesResponseType(typeof(CheckAuthCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CheckAuth([FromBody] CheckUserAuthRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<CheckAuthCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}
	}
}