using MediatR;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryRequest : IRequest<VerifyCodeQueryResponse>
    {
        public string Email { get; init; }
        public string Role { get; init; }
        public string Code { get; init; }
    }
}