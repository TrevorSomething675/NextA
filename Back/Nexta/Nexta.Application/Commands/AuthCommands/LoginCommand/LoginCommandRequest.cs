using MediatR;

namespace Nexta.Application.Commands.AuthCommands.LoginCommand
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}