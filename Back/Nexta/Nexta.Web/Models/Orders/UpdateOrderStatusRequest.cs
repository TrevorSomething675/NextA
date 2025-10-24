using Nexta.Domain.Enums;

namespace Nexta.Web.Models.Orders
{
    public class UpdateOrderStatusRequest
    {
         public Guid OrderId { get; set; }
         public OrderStatus Status { get; set; }
    }
}