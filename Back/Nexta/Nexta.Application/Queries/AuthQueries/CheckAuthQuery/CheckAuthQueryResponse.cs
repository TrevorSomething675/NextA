using Nexta.Domain.Models;

namespace Nexta.Application.Queries.AuthQueries.CheckAuthQuery
{
    public class CheckAuthQueryResponse(User user, string accessToken)
    {
        public User User { get; set; } = user;
        public string AccessToken { get; set; } = accessToken;
    }
}