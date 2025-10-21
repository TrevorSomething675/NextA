using Nexta.Application.Queries.Products.GetProductByIdQuery;
using Nexta.Application.Queries.Products.GetProductsQuery;
using Nexta.Application.DTO.Product;
using Nexta.Web.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Nexta.Domain.Base;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("[action]/{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<IResult> GetById([FromRoute] Guid id, CancellationToken ct = default)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(PagedData<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IResult> Get([FromQuery] GetProductsRequest request, CancellationToken ct = default)
        {
            var query = _mapper.Map<GetProductsQuery>(request);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }
    }
}