using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nexta.Application.Queries.Details.GetDetailsQuery;
using Nexta.Application.Queries.Details.GetDetailByIdQuery;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class DetailController : ControllerBase
	{
		private readonly IMediator _mediator;
		public DetailController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetDetailByIdQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromBody] GetDetailByIdQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetDetailsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetAll([FromBody] GetDetailsQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}