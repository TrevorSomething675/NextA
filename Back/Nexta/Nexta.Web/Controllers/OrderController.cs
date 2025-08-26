using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Application.Commands.Orders.CreateNewOrderCommand;
using Nexta.Application.Commands.Orders.DeleteOrderCommand;
using Nexta.Application.Commands.Orders.UpdateOrderProductCommand;
using Nexta.Application.Queries.Orders.GetLegacyOrdersQuery;
using Nexta.Application.Queries.Orders.GetOrdersForUserQuery;
using Nexta.Web.Models.Orders;

namespace Nexta.Web.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public OrderController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(typeof(GetOrdersForUserQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetOrdersForUser([FromQuery] GetOrdersForUserRequest request, CancellationToken ct = default)
		{
			var query = _mapper.Map<GetOrdersForUserQuery>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}

		[HttpGet]
		[ProducesResponseType(typeof(GetLegacyOrdersQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetLegacyOrdersForUser([FromQuery] GetOrdersForUserRequest request, CancellationToken ct = default)
		{
			var query = _mapper.Map<GetLegacyOrdersQuery>(request);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[HttpPost]
		[ProducesResponseType(typeof(CreateNewOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> CreateNewOrder([FromBody] CreateNewOrderRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<CreateNewOrderCommand>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}

		[HttpPatch]
		[ProducesResponseType(typeof(UpdateOrderProductCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> UpdateOrderProduct([FromBody] UpdateOrderProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<UpdateOrderProductCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpDelete("{orderId}")]
		[ProducesResponseType(typeof(DeleteOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromRoute] Guid orderId, CancellationToken ct = default)
		{
			var command = new DeleteOrderCommand(orderId);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}
	}
}