using MediatR;

namespace Nexta.Application.Queries.Auth.IsRegistrationQuery
{
    public class IsRegistrationQuery(string email) : IRequest<Unit>
    {
        public string Email { get; init; } = email;
    }
}