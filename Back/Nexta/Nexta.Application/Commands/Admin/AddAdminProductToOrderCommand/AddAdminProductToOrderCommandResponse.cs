using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandResponse(OrderProductResponse orderProduct)
    {
        public OrderProductResponse OrderProduct { get; init; } = orderProduct;
    }
}