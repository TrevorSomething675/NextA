using Nexta.Application.Commands.Admin.DeleteNewsCommand;
using Nexta.Application.Commands.Admin.AddNewsCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AddNewsCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Add([FromBody] AddNewsCommandRequest request, CancellationToken ct = default)
		{
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
		}

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(DeleteNewsCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Delete([FromBody] DeleteNewsCommandRequest request, CancellationToken ct = default)
        {
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
        }
    }
}