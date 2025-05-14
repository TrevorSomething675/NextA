using Nexta.Application.Queries.GetDetailByIdQuery;
using Nexta.Application.Queries.GetDetailsQuery;
using Microsoft.AspNetCore.Mvc;
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

		[HttpPost("[action]")]
		public async Task<IActionResult> Get([FromBody] GetDetailByIdQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> GetAll([FromBody] GetDetailsQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
	}
}