using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Auth.IsAuthenticatedQuery
{
    public class IsAuthenticatedQueryResponse(User user, string accessToken)
    {
        public User User { get; set; } = user;
        public string AccessToken { get; set; } = accessToken;
    }
}