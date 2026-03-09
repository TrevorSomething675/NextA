namespace Nexta.Application.Abstractions
{
    public interface IHashService
    {
        string? Generate(string data);
        bool Validate(string data, string hashedData);
    }
}