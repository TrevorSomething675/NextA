namespace Nexta.Domain.Abstractions.Services
{
    public interface IJwtTokenService
    {
		string CreateAccessToken(string email, string role);
		string CreateRefreshToken();
	}
}