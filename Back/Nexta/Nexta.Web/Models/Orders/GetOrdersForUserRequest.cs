using Nexta.Domain.Enums;

namespace Nexta.Web.Models.Orders
{
    public class GetOrdersForUserRequest
    {
        public Guid UserId { get; set; }
        public List<OrderStatus> Statuses { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}