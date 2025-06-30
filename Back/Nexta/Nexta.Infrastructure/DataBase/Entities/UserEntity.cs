using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
	public class UserEntity : BaseEntity
	{
		public string FirstName { get; set; } = null!;
		public string MiddleName { get; set; } = null!;
		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;
		public int? Phone { get; set; }

		public string PasswordHash { get; set; } = null!;
		public Role Role { get; set; } = Role.Unknown;
		public List<UserDetailEntity>? UserDetails { get; set; }

		public List<OrderEntity>? Orders { get; set; }
	}
}