using Nexta.Domain.Abstractions.Services;

namespace Nexta.Infrastructure.Security
{
	public class PasswordHashService : IHashService
	{
		public string? Generate(string data)
		{
			var hashedData = BCrypt.Net.BCrypt.EnhancedHashPassword(data);
			return hashedData;
		}

		public bool Validate(string data, string hashedData)
		{
			return BCrypt.Net.BCrypt.EnhancedVerify(data, hashedData);
		}
	}
}