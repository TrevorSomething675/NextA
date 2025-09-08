using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class Order
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; } = null!;

		public List<Product>? Products { get; set; }
		public List<OrderProduct>? OrderProducts { get; set; }

		public DateOnly CreatedDate { get; set; }
		public OrderStatus Status { get; set; }
	}
}