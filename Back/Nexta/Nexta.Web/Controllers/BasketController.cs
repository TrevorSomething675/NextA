using Nexta.Application.Commands.AddDetailToBasketCommand;
using Nexta.Application.Commands.DeleteDetailFromBasket;
using Nexta.Application.Queries.GetUserBasketDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
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
		public async Task<IActionResult> Add([FromBody] AddBasketDetailQueryRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Delete([FromBody] DeleteBasketDetailCommandRequest request, CancellationToken ct = default)
		{
			return (await _mediator.Send(request, ct)).ToActionResult();
		}
	}
}