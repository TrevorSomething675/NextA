using Nexta.Domain.Enums;

namespace Nexta.Domain.Entities
{
	public class UserDetailEntity
	{
		public Guid UserId { get; set; }
		public UserEntity User { get; set; } = null!;

		public Guid DetailId { get; set; }
		public DetailEntity Detail { get; set; } = null!;

		public int Count { get; set; }
		public DateOnly DeliveryDate { get; set; }
		public UserDetailStatus Status { get; set; }
	}
}