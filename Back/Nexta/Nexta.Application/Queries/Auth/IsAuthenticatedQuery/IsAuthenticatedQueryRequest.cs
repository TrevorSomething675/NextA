using MediatR;

namespace Nexta.Application.Queries.Auth.IsAuthenticatedQuery
{
    public class IsAuthenticatedQueryRequest : IRequest<IsAuthenticatedQueryResponse>
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}