using MediatR;

namespace Nexta.Application.Commands.Account.ConfirmPhoneCommand
{
    public class ConfirmPhoneCommandRequest : IRequest<ConfirmPhoneCommandResponse>
    {
        public string Email { get; init; }
        public string Phone { get; init; }
    }
}
