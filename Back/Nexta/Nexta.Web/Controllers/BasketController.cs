using Nexta.Application.Commands.AddDetailToBasketCommand;
using Nexta.Application.Commands.DeleteDetailFromBasket;
using Nexta.Application.Queries.GetUserBasketDetails;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Route("[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IMediator _mediator;
		public BasketController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Get([FromBody] GetBasketDetailsQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Add([FromHeader] AddBasketDetailQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Delete([FromHeader] DeleteBasketDetailCommandRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
	}
}