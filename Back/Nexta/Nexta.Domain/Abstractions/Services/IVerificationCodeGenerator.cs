namespace Nexta.Domain.Abstractions.Services
{
    public interface IVerificationCodeGenerator
    {
        string GenerateCode();
        DateTime GenerateExpiryTime();
    }
}