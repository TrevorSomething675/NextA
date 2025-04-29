using Nexta.Domain.Models;

namespace Nexta.Application.Commands.RegistrationCommand
{
    public class RegistrationCommandResponse(User user, string accessToken, string refreshToken)
    {
        public User User { get; set; } = user;

        public string RefreshToken { get; set; } = refreshToken;
        public string AccessToken { get; set; } = accessToken;
    }
}