using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
    public class OrderEntity : BaseEntity
    {
		public Guid UserId { get; set; }
		public UserEntity User { get; set; } = null!;

		public List<OrderProductEntity>? OrderProducts { get; set; }

		public List<ProductEntity>? Products { get; set; }

		public DateOnly CreatedDate { get; set; }
		public OrderStatus Status { get; set; }
	}
}