using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommandResponse(User user, string accessToken)
    {
        public User User { get; set; } = user;
        public string AccessToken { get; set; } = accessToken;
    }
}