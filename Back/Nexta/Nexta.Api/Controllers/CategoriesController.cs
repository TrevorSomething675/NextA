using Nexta.Application.Queries.Categories.GetCategoriesQuery;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(GetCategoriesQueryResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Get(CancellationToken ct = default)
        {
            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }
    }
}