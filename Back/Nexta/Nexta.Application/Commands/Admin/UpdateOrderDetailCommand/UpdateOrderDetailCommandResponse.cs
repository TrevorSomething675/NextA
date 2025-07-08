using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Admin.UpdateOrderDetailCommand
{
    public class UpdateOrderDetailCommandResponse(OrderDetailResponse orderDetail)
    {
        public OrderDetailResponse OrderDetail { get; init; } = orderDetail;
    }
}