using Nexta.Application.Commands.Account.ChangePasswordCommand;
using Nexta.Application.Commands.Account.ConfirmPhoneCommand;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Nexta.Web.Models.Account;
using Nexta.Web.Areas.Models;
using Nexta.Application.Commands.Auth.AccessRecoveryCommand;
using Microsoft.AspNetCore.Authorization;
using Nexta.Application.Commands.Account.UpdateAccountCommand;
using Nexta.Application.Commands.Account.UpdateEmailCommand;

namespace Nexta.Web.Controllers
{
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ConfirmPhoneCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> ConfirmPhone([FromBody] ConfirmPhoneRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<ConfirmPhoneCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<ChangePasswordCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IResult> AccessRecovery([FromBody] AccessRecoveryRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<AccessRecoveryCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [Authorize]
        [HttpPatch("[action]")]
        [ProducesResponseType(typeof(UpdateAccountCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> UpdateEmail([FromBody] UpdateEmailRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<UpdateEmailCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [Authorize]
        [HttpPatch("[action]")]
        [ProducesResponseType(typeof(UpdateAccountCommandResponse), StatusCodes.Status200OK)]
        public async Task<IResult> Update([FromBody] UpdateAccountRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<UpdateAccountCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}