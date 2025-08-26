using Nexta.Application.Commands.Basket.UpdateBasketProductCommand;
using Nexta.Application.Commands.Basket.DeleteBasketProductCommand;
using Nexta.Application.Commands.Basket.AddBasketProductCommand;
using Nexta.Application.Queries.Basket.GetBasketProductsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Web.Models.Basket;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	public class BasketController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public BasketController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("[action]")]
		[ProducesResponseType(typeof(GetBasketProductsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromQuery] GetBasketProductsRequest request, CancellationToken ct = default)
		{
			var query = _mapper.Map<GetBasketProductsQuery>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(AddBasketProductCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Add([FromBody] AddBasketProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<AddBasketProductCommand>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}

		[HttpPatch("[action]")]
		[ProducesResponseType(typeof(UpdateBasketProductCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Update([FromBody] UpdateBasketProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<UpdateBasketProductCommand>(request);
			var response = await _mediator.Send(command, ct);

			return Results.Ok(response);
		}

		[HttpPost("[action]")]
		[ProducesResponseType(typeof(DeleteBasketProductCommandResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Delete([FromRoute] DeleteBasketProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<DeleteBasketProductCommand>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}
	}
}