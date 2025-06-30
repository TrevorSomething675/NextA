using Nexta.Domain.Enums;

namespace Nexta.Application.DTO
{
    public class UserResponse
    {
		public Guid Id { get; init; }
		public string? FirstName { get; init; }
		public string? MiddleName { get; init; }
		public string? LastName { get; init; }

		public string? Email { get; init; }
		public int? Phone { get; init; }

		public Role Role { get; init; }
	}
}