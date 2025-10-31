namespace Nexta.Domain.Services.VerificationCode
{
    public interface IVerificationCodeGenerator
    {
        string GenerateCode();
        DateTime GenerateExpiryTime();
    }
}