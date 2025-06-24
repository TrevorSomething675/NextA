using Nexta.Application.Queries.Auth.SendVerificationCodeQuery;
using Nexta.Application.Queries.Auth.VerifyCodeQuery;
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
		[ProducesResponseType(typeof(SendVerificationCodeQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> SendVerificationCode([FromBody] SendVerificationCodeQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(VerifyCodeQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> VerifyCode([FromForm] VerifyCodeQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}