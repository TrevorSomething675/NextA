using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Application.Commands.Auth.CheckAuthCommand;
using Nexta.Application.Commands.Auth.LoginCommand;
using Nexta.Application.Commands.Auth.RegistrationCommand;
using Nexta.Application.Queries.Auth.IsRegistrationQuery;
using Nexta.Web.Models.Auth;

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

		[HttpPost("login")]
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Login([FromBody] LoginRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<LoginCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpPost("registration")]
		[ProducesResponseType(typeof(RegistrationCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> RegistrationAsync([FromBody] RegistrationRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<RegistrationCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response); 
		}

        [HttpGet("isRegistration")]
		[ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
		public async Task<IResult> IsRegistrationUserAsync([FromQuery] string email, CancellationToken ct = default)
		{
			var query = new IsRegistrationQuery(email);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[Authorize]
		[HttpPost("registreation-status")]
		[ProducesResponseType(typeof(CheckAuthCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> IsAuthAsync([FromBody] CheckUserAuthRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<CheckAuthCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}
	}
}