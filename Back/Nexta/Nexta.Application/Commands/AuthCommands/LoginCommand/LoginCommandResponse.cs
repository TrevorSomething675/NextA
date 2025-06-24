using Nexta.Domain.Models;

namespace Nexta.Application.Commands.AuthCommands.LoginCommand
{
    public class LoginCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}