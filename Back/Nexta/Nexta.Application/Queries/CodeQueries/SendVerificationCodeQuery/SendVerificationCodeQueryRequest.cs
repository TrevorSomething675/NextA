using MediatR;

namespace Nexta.Application.Queries.CodeQueries.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryRequest : IRequest<SendVerificationCodeQueryResponse>
    {
        public string Email { get; set; }
    }
}