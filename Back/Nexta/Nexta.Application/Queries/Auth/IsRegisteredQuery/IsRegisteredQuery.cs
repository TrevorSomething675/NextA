using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQuery(string email) : IRequest<Unit>
    {
        public string Email { get; init; } = email;
    }
}