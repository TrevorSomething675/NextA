using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nexta.Application.Queries.Details.GetWarehouseDetailsQuery;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class WarehouseController : ControllerBase
    {
		private readonly IMediator _mediator;
		public WarehouseController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetWarehouseDetailsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromBody] GetWarehouseDetailsQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
    }
}