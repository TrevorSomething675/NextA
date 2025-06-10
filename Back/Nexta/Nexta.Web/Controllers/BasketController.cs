using Nexta.Application.Commands.AddDetailToBasketCommand;
using Nexta.Application.Commands.DeleteDetailFromBasket;
using Nexta.Application.Queries.GetUserBasketDetails;
using Nexta.Application.Commands.LoginCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IMediator _mediator;
		public BasketController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromBody] GetBasketDetailsQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Add([FromBody] AddBasketDetailQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(LoginCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromBody] DeleteBasketDetailCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}