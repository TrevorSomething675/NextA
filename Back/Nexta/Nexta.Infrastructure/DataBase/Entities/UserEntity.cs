namespace Nexta.Infrastructure.DataBase.Entities
{
	public class UserEntity : BaseEntity
	{
		public string FirstName { get; set; } = null!;
		public string MiddleName { get; set; } = null!;
		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;
		public string? Phone { get; set; }

		public string PasswordHash { get; set; } = null!;
		public string Role { get; set; } = "User";

		public ICollection<BasketProductEntity> BasketProducts { get; set; } = new List<BasketProductEntity>();

		public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

		public ICollection<NotificationEntity> Notifications { get; set; } = new List<NotificationEntity>();
	}
}