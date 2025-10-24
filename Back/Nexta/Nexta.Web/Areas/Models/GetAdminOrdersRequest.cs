using Nexta.Domain.Enums;

namespace Nexta.Web.Areas.Models
{
    public class GetAdminOrdersRequest
    {
        public List<OrderStatus> Statuses { get; init; }
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 8;
    }
}
