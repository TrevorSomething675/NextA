using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryRequest : IRequest<IsRegisteredQueryResponse>
    {
        public string Email { get; set; }
    }
}