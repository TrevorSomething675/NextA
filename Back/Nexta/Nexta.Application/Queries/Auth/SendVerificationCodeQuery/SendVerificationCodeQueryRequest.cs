using MediatR;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryRequest : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}