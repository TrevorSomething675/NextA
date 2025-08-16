using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryResponse(UserResponse user, string accessToken)
    {
        public UserResponse User { get; init; } = user;
        public string AccessToken { get; init; } = accessToken;
    }
}