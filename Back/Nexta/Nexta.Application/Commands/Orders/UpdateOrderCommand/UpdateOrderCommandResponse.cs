using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Orders.UpdateOrderCommand
{
    public class UpdateOrderCommandResponse(OrderResponse order)
    {
        public OrderResponse Order { get; set; } = order;
    }
}