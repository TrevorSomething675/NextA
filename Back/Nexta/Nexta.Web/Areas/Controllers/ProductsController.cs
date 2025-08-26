using Nexta.Application.Commands.Admin.CreateAdminProductCommand;
using Nexta.Application.Commands.Admin.UpdateDetailCommand;
using Nexta.Application.Queries.Admin.GetProductsQuery;
using Nexta.Application.Queries.Admin.GetProductQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Web.Areas.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
	[ApiController]
    public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(GetAdminProductQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken ct = default)
		{
			var query = new GetAdminProductQuery(id, true);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[HttpGet]
		[ProducesResponseType(typeof(GetAdminProductsQueryResponse), StatusCodes.Status200OK)]
		public async Task<IResult> Get([FromQuery] GetAdminProductsRequest request, CancellationToken ct = default)
		{
			var query = _mapper.Map<GetAdminProductsQuery>(request);
			var response = await _mediator.Send(query, ct);

			return Results.Ok(response);
		}

		[HttpPatch]
		public async Task<IResult> Update([FromBody] UpdateAdminProductRequest request, CancellationToken ct = default)
		{
			var command = _mapper.Map<UpdateAdminProductCommand>(request);
			var response = await _mediator.Send(request, ct);

			return Results.Ok(response);
		}

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(CreateAdminProductCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] CreateAdminProductRequest request, CancellationToken ct = default)
        {
			var command = _mapper.Map<CreateAdminProductCommand>(request);
            var response = await _mediator.Send(request, ct);

            return Results.Ok(response);
        }
    }
}