using Nexta.Application.Queries.News.GetNewsQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class NewsController : Controller
    {
		private readonly IMediator _mediator;

		public NewsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("Get")]
		[ProducesResponseType(typeof(GetNewsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get(CancellationToken ct = default)
		{
			var response = await _mediator.Send(new GetNewsQueryRequest(), ct);
			return Results.Ok(response);
		}
	}
}