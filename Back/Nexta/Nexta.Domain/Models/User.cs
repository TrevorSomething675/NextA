using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class User
	{
		public Guid Id { get; set; }
		public string? FirstName { get; set; }
		public string? MiddleName { get; set; }
		public string? LastName { get; set; }
					 
		public string? Email { get; set; }
		public int? Phone { get; set; }

		public string? PasswordHash { get; set; }
		public Role Role { get; set; } = Role.Unknown;
	}
}
