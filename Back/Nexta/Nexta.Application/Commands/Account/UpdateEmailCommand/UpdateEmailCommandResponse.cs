using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Account.UpdateEmailCommand
{
    public class UpdateEmailCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}
