namespace Nexta.Application.Interfaces
{
    public interface IJwtTokenService
    {
		string CreateAccessToken(string email, string role);
		string CreateRefreshToken();
	}
}