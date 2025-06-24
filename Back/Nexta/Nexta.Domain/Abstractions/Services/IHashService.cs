namespace Nexta.Domain.Abstractions.Services
{
    public interface IHashService
    {
        string? Generate(string data);
        bool Validate(string data, string hashedData);
    }
}