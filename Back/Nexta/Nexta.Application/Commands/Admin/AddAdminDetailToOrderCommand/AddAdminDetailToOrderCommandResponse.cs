using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Admin.AddAdminDetailToOrderCommand
{
    public class AddAdminDetailToOrderCommandResponse(OrderDetailResponse orderDetail)
    {
        public OrderDetailResponse OrderDetail { get; init; } = orderDetail;
    }
}
