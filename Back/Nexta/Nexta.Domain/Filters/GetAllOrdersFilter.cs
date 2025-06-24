using Nexta.Domain.Enums;

namespace Nexta.Domain.Filters
{
    public class GetAllOrdersFilter : BaseFilter
    {
		public List<OrderStatus> Statuses { get; set; }
	}
}