using Nexta.Application.Commands.Users.DeleteUserCommand;
using Nexta.Application.Queries.Admin.GetUsersQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nexta.Web.Models.Users;
using AutoMapper;
using MediatR;

namespace Nexta.Web.Areas.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(GetUsersQueryResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Get([FromQuery] GetUsersRequest request, CancellationToken ct = default)
        {
            var query = _mapper.Map<GetUsersQuery>(request);
            var response = await _mediator.Send(query, ct);

            return Results.Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(GetUsersQueryResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
        {
            var command = new DeleteUserCommand(id);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}