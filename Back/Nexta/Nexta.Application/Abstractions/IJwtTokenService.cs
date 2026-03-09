namespace Nexta.Application.Abstractions
{
    public interface IJwtTokenService
    {
		string CreateAccessToken(string email, string role);
		string CreateRefreshToken();
	}
}