using Nexta.Domain.Models;

namespace Nexta.Application.Commands.AuthCommands.RegistrationCommand
{
    public class RegistrationCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}