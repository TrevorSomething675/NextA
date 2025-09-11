using MediatR;

namespace Nexta.Application.Commands.Account.ChangePasswordCommand
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public Guid UserId { get; init; }
        public string Email { get; init; }
        public string LegacyPassword { get; init; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
    }
}