using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nexta.Application.Queries.Orders.GetLegacyOrdersQuery;
using Nexta.Application.Queries.Orders.GetOrdersForUserQuery;
using Nexta.Application.Commands.Orders.CreateNewOrderCommand;
using Nexta.Application.Commands.Orders.DeleteOrderCommand;
using Nexta.Application.Commands.Admin.UpdateOrderDetailCommand;

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
		[ProducesResponseType(typeof(GetOrdersForUserQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetOrdersForUser([FromBody] GetOrdersForUserQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetLegacyOrdersQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetLegacyOrdersForUser([FromBody] GetLegacyOrdersQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(CreateNewOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CreateNewOrder([FromBody] CreateNewOrderCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(DeleteOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromBody] DeleteOrderCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(UpdateOrderDetailCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> UpdateOrderDetail([FromBody] UpdateOrderDetailCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}