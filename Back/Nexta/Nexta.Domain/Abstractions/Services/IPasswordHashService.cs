namespace Nexta.Domain.Abstractions.Services
{
    public interface IPasswordHashService
    {
        string? Generate(string password);
        bool Validate(string passwordHash, string hash);
    }
}
