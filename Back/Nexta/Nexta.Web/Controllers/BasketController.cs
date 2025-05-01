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

		[HttpPost(nameof(AddBasketDetail))]
		public async Task<IActionResult> AddBasketDetail([FromHeader] AddBasketDetailQueryRequest request, CancellationToken cancellationToken = default)
		{
			return (await _mediator.Send(request, cancellationToken)).ToActionResult();
		}

		[HttpPost(nameof(DeleteBasketDetail))]
		public async Task<IActionResult> DeleteBasketDetail([FromHeader] DeleteBasketDetailCommandRequest request, CancellationToken cancellationToken = default)
		{
			return (await _mediator.Send(request, cancellationToken)).ToActionResult();
		}

		[HttpPost(nameof(GetBasketDetails))]
		public async Task<IActionResult> GetBasketDetails([FromBody] GetBasketDetailsQueryRequest request, CancellationToken cancellationToken = default)
		{
			return (await _mediator.Send(request, cancellationToken)).ToActionResult();
		}
	}
}