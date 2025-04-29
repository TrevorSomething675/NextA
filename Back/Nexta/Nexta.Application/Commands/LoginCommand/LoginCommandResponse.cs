using Nexta.Domain.Models;

namespace Nexta.Application.Commands.LoginCommand
{
    public class LoginCommandResponse(User user, string accessToken, string refreshToken)
    {
        public User User { get; set; } = user;

        public string AccessToken { get; set; } = accessToken;
        public string RefreshToken { get; set; } = refreshToken;
    }
}