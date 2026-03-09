namespace Nexta.Application.Abstractions
{
    public interface IVerificationCodeService
    {
		string SetVerificationCode(string email);
        bool VerifyCode(string email, string code);
    }
}