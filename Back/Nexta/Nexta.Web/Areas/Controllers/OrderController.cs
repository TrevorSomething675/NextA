using Nexta.Application.Commands.Admin.AddAdminDetailToOrderCommand;
using Nexta.Application.Commands.Admin.DeleteDetailFromOrderCommand;
using Nexta.Application.Commands.Admin.UpdateOrderCommand;
using Nexta.Application.Queries.Admin.GetAllOrdersQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
    {
		private readonly IMediator _mediator;
		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(DeleteDetailFromOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromBody] DeleteDetailFromOrderCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(UpdateAdminOrderCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Update([FromBody] UpdateAdminOrderCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AddAdminDetailToOrderCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] AddAdminDetailToOrderCommandRequest request, CancellationToken ct = default)
        {
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(GetAdminOrdersQueryResponse), StatusCodes.Status200OK)]
        public async Task<IResult> GetAllOrders(GetAdminOrdersQueryRequest request, CancellationToken ct = default)
        {
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
        }
    }
}