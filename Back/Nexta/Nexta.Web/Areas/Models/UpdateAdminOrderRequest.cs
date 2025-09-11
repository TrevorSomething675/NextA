using Nexta.Application.DTO.Request;
using Nexta.Domain.Enums;

namespace Nexta.Web.Areas.Models
{
    public class UpdateAdminOrderRequest
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public List<OrderProductsRequest>? OrderProducts { get; init; }
    }
}
