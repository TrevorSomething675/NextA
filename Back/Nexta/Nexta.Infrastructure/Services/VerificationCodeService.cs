using Microsoft.Extensions.Caching.Memory;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models.CacheModels;

namespace Nexta.Application.Services
{
	public class VerificationCodeService : IVerificationCodeService
	{
		private readonly IVerificationCodeGenerator _codeGenerator;
		private readonly IMemoryCache _cache;

		public VerificationCodeService(IVerificationCodeGenerator codeGenerator, IMemoryCache cache)
		{
			_codeGenerator = codeGenerator;
			_cache = cache;
		}

		public EmailVerificationCode SetVerificationCode(string email)
		{
			try
			{
				var code = _codeGenerator.GenerateCode();
				var expiryTime = _codeGenerator.GenerateExpiryTime();

				var verificationCode = new EmailVerificationCode
				{
					Email = email,
					Code = code,
					ExpiryTime = expiryTime,
					IsVerified = false
				};

				_cache.Set(email, verificationCode, TimeSpan.FromMinutes(3));
				return verificationCode;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool VerifyCode(string email, string code)
		{
			if (!_cache.TryGetValue(email, out EmailVerificationCode verificationCode))
				throw new BadRequestException("Не удалось получить код");

			if (verificationCode.IsVerified || verificationCode.ExpiryTime < DateTime.UtcNow)
				throw new BadRequestException("Код просрочен");

			if (verificationCode.Code != code)
				throw new BadRequestException("Неверный код");

			verificationCode.IsVerified = true;
			_cache.Set(email, verificationCode, TimeSpan.FromMinutes(3));

			return true;
		}
	}
}