namespace Nexta.Domain.Abstractions.Services
{
    public interface IJwtTokenService
    {
		string CreateAccessToken(Guid id, string role);
		string CreateRefreshToken();
	}
}