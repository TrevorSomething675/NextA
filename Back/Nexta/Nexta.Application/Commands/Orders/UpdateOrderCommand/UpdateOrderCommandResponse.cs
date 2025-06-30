using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Orders.UpdateOrderCommand
{
    public class UpdateOrderCommandResponse(User user)
    {
        public User User { get; set; } = user;
    }
}