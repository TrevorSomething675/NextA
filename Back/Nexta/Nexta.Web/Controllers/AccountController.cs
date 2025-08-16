using Nexta.Application.Commands.Account.ChangePasswordCommand;
using Nexta.Application.Commands.Account.ConfirmPhoneCommand;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ConfirmPhoneCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> ConfirmPhone([FromForm] ConfirmPhoneCommandRequest request, CancellationToken ct = default)
        {
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IResult> ChangePassword([FromBody] ChangePasswordCommandRequest request, CancellationToken ct = default)
        {
            var response = await _mediator.Send(request, ct);
            return Results.Ok(response);
        }
    }
}
