using Nexta.Application.Commands.CreateNewOrderCommand;
using Nexta.Application.Queries.GetLegacyOrdersQuery;
using Nexta.Application.Queries.GetOrdersQuery;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Nexta.Web.Controllers
{
	[Authorize]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> GetOrdersForUser([FromBody] GetOrdersForUserQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> GetLegacyOrdersForUser([FromBody] GetLegacyOrdersQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateNewOrder([FromBody] CreateNewOrderCommandRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
	}
}