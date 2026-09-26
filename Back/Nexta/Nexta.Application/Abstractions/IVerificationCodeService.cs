using Nexta.Domain.Models.CacheModels;

namespace Nexta.Domain.Abstractions.Services
{
    public interface IVerificationCodeService
    {
		EmailVerificationCode SetVerificationCode(string email);
        bool VerifyCode(string email, string code);
    }
}