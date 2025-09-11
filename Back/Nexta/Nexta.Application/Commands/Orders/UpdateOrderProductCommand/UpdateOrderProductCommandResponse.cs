using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Orders.UpdateOrderProductCommand
{
    public class UpdateOrderProductCommandResponse(OrderProductResponse orderProduct)
    {
        public OrderProductResponse OrderProduct { get; init; } = orderProduct;
    }
}