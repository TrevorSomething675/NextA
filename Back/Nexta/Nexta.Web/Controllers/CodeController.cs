using Nexta.Application.Queries.Auth.SendVerificationCodeQuery;
using Nexta.Application.Commands.Auth.VerifyCodeQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class CodeController : Controller
	{
		private readonly IMediator _mediator;
		public CodeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
		public async Task<IResult> SendVerificationCode([FromBody] SendVerificationCodeQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(VerifyCodeCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> VerifyCode([FromBody] VerifyCodeCommand request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
    }
}