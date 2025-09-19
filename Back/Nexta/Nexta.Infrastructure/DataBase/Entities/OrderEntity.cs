using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
    public class OrderEntity : BaseEntity
    {
		public Guid UserId { get; set; }
		public UserEntity User { get; set; } = null!;

		public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
		public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();

		public DateOnly CreatedDate { get; set; }
		public OrderStatus Status { get; set; }
	}
}