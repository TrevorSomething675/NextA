namespace Nexta.Domain.Filters
{
    public class GetOrdersFilter : BaseFilter
    {
        public string SearchTerm { get; set; }
        public Guid UserId { get; set; }
	}
}