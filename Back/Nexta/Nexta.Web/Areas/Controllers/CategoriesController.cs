using Nexta.Application.Commands.Categories.DeleteCategoryCommand;
using Nexta.Application.Commands.Categories.AddCategoryCommand;
using Microsoft.AspNetCore.Authorization;
using Nexta.Web.Models.Categories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] AddCategoryRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<AddCategoryCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IResult> Delete([FromQuery] DeleteCategoryRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<DeleteCategoryCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}
