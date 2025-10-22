using Nexta.Application.Commands.Admin.DeleteNewsCommand;
using Nexta.Application.Commands.Admin.AddNewsCommand;
using Microsoft.AspNetCore.Authorization;
using Nexta.Application.Commands.News;
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
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public NewsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            var command = new DeleteNewsCommandRequest(id);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(NewsDto), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] AddNewsRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<AddNewsCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}