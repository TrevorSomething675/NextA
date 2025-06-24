using Nexta.Application.Queries.Admin.GetAllOrdersQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
	[Route("admin/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
    {
		private readonly IMediator _mediator;
		public HomeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(GetAllOrdersQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetAllOrders(GetAllOrdersQueryRequest request, CancellationToken ct = default)
		{
			var response = await _mediator.Send(request, ct);
			return Results.Ok(response);
		}
	}
}