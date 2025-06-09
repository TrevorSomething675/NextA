namespace Nexta.Domain.Filters
{
    public class OrdersFilter : BaseFilter
    {
        public Guid UserId { get; set; }
		public int PageSize { get; set; } = 8;
	}
}