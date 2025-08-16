using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Auth.IsAuthenticatedQuery
{
    public class IsAuthenticatedQueryResponse(UserResponse user, string accessToken)
    {
        public UserResponse? User { get; init; } = user;
        public string? AccessToken { get; init; } = accessToken;
    }
}