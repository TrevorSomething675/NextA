using Nexta.Domain.Enums;

namespace Nexta.Web.Areas.Models
{
    public class GetAdminOrdersRequest
    {
        public List<OrderStatus> Statuses { get; set; }
        public string? SearchTerm { get; init; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
