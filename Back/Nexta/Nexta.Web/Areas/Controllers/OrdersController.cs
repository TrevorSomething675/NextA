using Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand;
using Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.Queries.Admin.GetAllOrdersQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Web.Areas.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
	[ApiController]
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
        [ProducesResponseType(typeof(GetAdminOrdersQueryResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Get([FromQuery] GetAdminOrdersRequest request, CancellationToken ct = default)
        {
			var query = _mapper.Map<GetAdminOrdersQuery>(request);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AddAdminProductToOrderCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] AddAdminProductToOrderRequest request, CancellationToken ct = default)
        {
			var command = _mapper.Map<AddAdminProductToOrderCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

		[HttpPatch("[action]")]
		[ProducesResponseType(typeof(UpdateAdminOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Update([FromBody] UpdateAdminOrderRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<UpdateAdminOrderCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(DeleteProductFromOrderCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Delete([FromQuery] DeleteProductFromOrderRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<DeleteProductFromOrderCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}