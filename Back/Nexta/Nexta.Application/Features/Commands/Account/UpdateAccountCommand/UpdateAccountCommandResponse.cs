using Nexta.Domain.Models.Users;

namespace Nexta.Application.Commands.Account.UpdateAccountCommand
{
    public class UpdateAccountCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}
