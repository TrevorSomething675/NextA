using MediatR;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryRequest : IRequest<SendVerificationCodeQueryResponse>
    {
        public string Email { get; set; }
    }
}