using Nexta.Application.DTO.Orders;
using Nexta.Domain.Enums;

namespace Nexta.Web.Areas.Models
{
    public class UpdateAdminOrderRequest
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public List<OrderItemDto> Products { get; init; }
    }
}
