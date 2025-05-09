using Nexta.Application.Queries.GetWarehouseDetailsQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
		public async Task<IActionResult> Get([FromBody] GetWarehouseDetailsQueryRequest request, CancellationToken ct = default)
        {
			return (await _mediator.Send(request, ct)).ToActionResult();
        }
    }
}