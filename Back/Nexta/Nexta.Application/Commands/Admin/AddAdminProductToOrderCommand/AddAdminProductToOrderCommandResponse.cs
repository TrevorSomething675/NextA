using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandResponse(OrderProductResponse orderDetail)
    {
        public OrderProductResponse OrderDetail { get; init; } = orderDetail;
    }
}
