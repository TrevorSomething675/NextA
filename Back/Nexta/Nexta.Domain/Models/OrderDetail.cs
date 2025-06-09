namespace Nexta.Domain.Models
{
    public class OrderDetail
    {
		public int Count { get; set; }
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = null!;

		public Guid DetailId { get; set; }
		public Detail Detail { get; set; } = null!;
	}
}