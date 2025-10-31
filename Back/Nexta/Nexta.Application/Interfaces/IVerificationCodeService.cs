namespace Nexta.Application.Interfaces
{
    public interface IVerificationCodeService
    {
		string SetVerificationCode(string email);
        bool VerifyCode(string email, string code);
    }
}