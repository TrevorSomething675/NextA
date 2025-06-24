using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nexta.Application.Queries.Basket.GetBasketDetailsQuery;
using Nexta.Application.Commands.Basket.DeleteBasketDetailCommand;
using Nexta.Application.Commands.Basket.AddBasketDetailCommand;

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
		[ProducesResponseType(typeof(GetBasketDetailsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromBody] GetBasketDetailsQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(AddBasketDetailQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Add([FromBody] AddBasketDetailQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(DeleteBasketDetailCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromBody] DeleteBasketDetailCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}