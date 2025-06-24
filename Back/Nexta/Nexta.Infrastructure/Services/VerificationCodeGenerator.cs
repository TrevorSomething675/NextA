using Nexta.Domain.Abstractions.Services;

namespace Nexta.Infrastructure.Services
{
	public class VerificationCodeGenerator : IVerificationCodeGenerator
	{
		public DateTime GenerateExpiryTime()
		{
			var date = DateTime.UtcNow.AddMinutes(1);
			return date;
		}

		public string GenerateCode()
		{
			var code = new Random().Next(100000, 999999).ToString();
			return code;
		}
	}
}