using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryRequest : IRequest<Unit>
    {
        public string Email { get; set; } = null!;
    }
}