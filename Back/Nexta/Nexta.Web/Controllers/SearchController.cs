using Nexta.Application.Queries.SearchDetailQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class SearchController : ControllerBase
    {
		private readonly IMediator _mediator;
		public SearchController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost("[action]")]
		[ProducesResponseType(typeof(SearchDetailQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> SearchDetail([FromBody]SearchDetailQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
    }
}