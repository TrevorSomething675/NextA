using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class Order
    {
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; } = null!;

		public List<OrderDetail>? OrderDetails { get; set; }

		public OrderStatus Status { get; set; }
	}
}