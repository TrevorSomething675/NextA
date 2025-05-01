using Nexta.Application.Queries.GetDetailsQuery;
using Microsoft.AspNetCore.Mvc;
using Nexta.Domain.Filters;
using MediatR;

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

		[HttpGet(nameof(GetAll))]
		public async Task<IActionResult> GetAll([FromHeader] GetDetailsQueryRequest request, CancellationToken cancellationToken = default)
		{
			return (await _mediator.Send(request, cancellationToken)).ToActionResult();
		}
	}
}