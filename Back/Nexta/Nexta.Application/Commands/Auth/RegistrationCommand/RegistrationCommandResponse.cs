using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Auth.RegistrationCommand
{
    public class RegistrationCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}