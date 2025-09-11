using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Auth.VerifyCodeQuery
{
    public class VerifyCodeCommandResponse(UserResponse user, string accessToken)
    {
        public UserResponse User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
    }
}