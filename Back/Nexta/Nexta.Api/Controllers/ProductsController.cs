using Nexta.Application.Queries.Products.GetProductByIdQuery;
using Nexta.Application.Queries.Products.GetProductsQuery;
using Nexta.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetProductByIdQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetByIdAsync([FromRoute] Guid id, CancellationToken ct = default)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetProductsQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetAsync([FromQuery] GetProductsRequest request, CancellationToken ct = default)
        {
            var query = _mapper.Map<GetProductsQuery>(request);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }
    }
}