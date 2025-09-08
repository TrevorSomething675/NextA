using Nexta.Application.Commands.Account.ChangePasswordCommand;
using Nexta.Application.Commands.Account.ConfirmPhoneCommand;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Nexta.Web.Models.Account;

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
        public async Task<IResult> ConfirmPhone([FromBody] ConfirmPhoneCommandRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<ConfirmPhoneCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        public async Task<IResult> ChangePassword([FromBody] ChangePasswordCommandRequest request, CancellationToken ct = default)
        {
            var command = _mapper.Map<ChangePasswordCommand>(request);
            var response = await _mediator.Send(command, ct);

            return Results.Ok(response);
        }
    }
}