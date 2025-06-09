using Nexta.Domain.Enums;

namespace Nexta.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
		public Guid UserId { get; set; }
		public UserEntity User { get; set; } = null!;

		public List<OrderDetailEntity>? OrderDetails { get; set; }
		public List<DetailEntity>? Details { get; set; }

		public OrderStatus Status { get; set; }
	}
}