using Nexta.Application.DTO.User;

namespace Nexta.Application.Commands.Auth.CheckAuthCommand
{
    public class CheckAuthCommandResponse(UserDto user, string accessToken)
    {
        public UserDto User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
    }
}