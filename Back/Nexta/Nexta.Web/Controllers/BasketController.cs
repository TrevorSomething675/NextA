using Nexta.Application.Commands.AddDetailToBasketCommand;
using Nexta.Application.Commands.DeleteDetailFromBasket;
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

		[HttpPost(nameof(AddDetailToBasket))]
		public async Task<IActionResult> AddDetailToBasket([FromHeader] AddDetailToBasketRequest request, CancellationToken cancellationToken = default)
		{
			return (await _mediator.Send(request, cancellationToken)).ToActionResult();
		}

		[HttpPost(nameof(DeleteDetailFromBasket))]
		public async Task<IActionResult> DeleteDetailFromBasket([FromHeader] DeleteDetailFromBasketHandler request, CancellationToken cancellationToken = default)
		{

		}
	}
}