using Nexta.Domain.Enums;

namespace Nexta.Domain.Filters
{
    public class GetOrdersFilter : BaseFilter
    {
        public Guid UserId { get; set; }
	}
}