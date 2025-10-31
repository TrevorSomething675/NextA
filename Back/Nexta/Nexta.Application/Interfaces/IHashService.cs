namespace Nexta.Application.Interfaces
{
    public interface IHashService
    {
        string? Generate(string data);
        bool Validate(string data, string hashedData);
    }
}