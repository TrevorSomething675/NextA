using MediatR;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQuery : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}