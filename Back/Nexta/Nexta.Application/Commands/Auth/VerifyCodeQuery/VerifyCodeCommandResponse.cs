using Nexta.Application.DTO.User;

namespace Nexta.Application.Commands.Auth.VerifyCodeQuery
{
    public class VerifyCodeCommandResponse(UserDto user, string accessToken)
    {
        public UserDto User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
    }
}