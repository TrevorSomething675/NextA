using Nexta.Domain.Enums;

namespace Nexta.Domain.Filters
{
    public class GetOrdersFilter : BaseFilter
    {
        public Guid UserId { get; set; }
		public int PageSize { get; set; } = 8;
        public List<OrderStatus> Statuses { get; set; }
	}
}