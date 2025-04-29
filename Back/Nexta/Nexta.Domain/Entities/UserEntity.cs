using Nexta.Domain.Enums;

namespace Nexta.Domain.Entities
{
	public class UserEntity : BaseEntity
	{
		public string FirstName { get; set; } = null!;
		public string MiddleName { get; set; } = null!;
		public string LastName { get; set; } = null!;

		public string Email { get; set; } = null!;
		public string PasswordHash { get; set; } = null!;
		public Role Role { get; set; } = Role.Unkown;
		public List<UserDetailEntity>? UserDetail { get; set; }
	}
}