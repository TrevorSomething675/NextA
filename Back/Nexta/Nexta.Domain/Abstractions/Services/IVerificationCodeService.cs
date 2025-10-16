namespace Nexta.Domain.Abstractions.Services
{
    public interface IVerificationCodeService
    {
		string SetVerificationCode(string email);
        bool VerifyCode(string email, string code);
    }
}