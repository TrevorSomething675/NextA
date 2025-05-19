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
		public async Task<IActionResult> SearchDetail([FromBody]SearchDetailQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
    }
}