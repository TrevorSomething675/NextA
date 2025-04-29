using Nexta.Domain.Abstractions.Services;

namespace Nexta.Application.Services
{
	public class PasswordHashService : IPasswordHashService
	{
		public string? Generate(string password)
		{
			var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
			return hashedPassword;
		}

		public bool Validate(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
		}
	}
}