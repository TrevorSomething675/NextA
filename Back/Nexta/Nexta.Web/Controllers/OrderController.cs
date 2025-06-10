using Nexta.Application.Commands.CreateNewOrderCommand;
using Nexta.Application.Queries.GetLegacyOrdersQuery;
using Nexta.Application.Queries.GetOrdersQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IResult> GetOrdersForUser([FromBody] GetOrdersForUserQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		public async Task<IResult> GetLegacyOrdersForUser([FromBody] GetLegacyOrdersQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		public async Task<IResult> CreateNewOrder([FromBody] CreateNewOrderCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}