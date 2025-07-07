using Nexta.Application.Commands.Admin.UpdateDetailCommand;
using Nexta.Application.Queries.Admin.GetDetailsQuery;
using Nexta.Application.Queries.Admin.GetDetailQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	[ApiController]
    public class DetailController : ControllerBase
	{
		private readonly IMediator _mediator;
		public DetailController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetAdminDetailQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromBody] GetAdminDetailQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetAdminDetailsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetAll([FromBody] GetAdminDetailsQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		public async Task<IResult> Update([FromBody] UpdateAdminDetailCommandRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}