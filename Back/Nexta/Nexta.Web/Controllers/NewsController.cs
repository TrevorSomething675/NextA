using Nexta.Application.Commands.Admin.AddNewsCommand;
using Nexta.Application.Queries.News.GetNewsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class NewsController : Controller
    {
		private readonly IMediator _mediator;

		public NewsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetNewsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetAll(CancellationToken ct = default)
		{
			var response = await _mediator.Send(new GetNewsQueryRequest(), ct);
			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(AddNewsCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Add(CancellationToken ct = default)
		{
			var response = await _mediator.Send(new AddNewsCommandRequest(), ct);
			return Results.Ok(response);
		}
	}
}