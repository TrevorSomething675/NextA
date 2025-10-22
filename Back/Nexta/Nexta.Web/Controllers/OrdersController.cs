using Nexta.Application.Commands.Orders.UpdateOrderProductCommand;
using Nexta.Application.Commands.Orders.CreateNewOrderCommand;
using Nexta.Application.Queries.Orders.GetOrdersForUserQuery;
using Nexta.Application.Commands.Orders.DeleteOrderCommand;
using Microsoft.AspNetCore.Authorization;
using Nexta.Application.DTO.Order;
using Microsoft.AspNetCore.Mvc;
using Nexta.Web.Models.Orders;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public OrdersController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(typeof(PagedData<OrderDto>), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromQuery] GetOrdersForUserRequest request, CancellationToken ct = default)
		{
			var query = _mapper.Map<GetOrdersForUserQuery>(request);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(CreateNewOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CreateNewOrder([FromBody] CreateNewOrderRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<CreateNewOrderCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpPatch("[action]")]
		[ProducesResponseType(typeof(OrderItemDto), StatusCodes.Status200OK)]
		public async Task<IResult> UpdateOrderProduct([FromBody] UpdateOrderProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<UpdateOrderProductCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpDelete("Delete/{OrderId}")]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromRoute] Guid OrderId, CancellationToken ct = default)
		{
			var command = new DeleteOrderCommand(OrderId);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}
	}
}