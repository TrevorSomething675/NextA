using Nexta.Application.DTO.Orders;

namespace Nexta.Web.Models.Orders
{
    public class CreateNewOrderRequest
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Products { get; set; }
    }
}