using MediatR;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryRequest : IRequest<VerifyCodeQueryResponse>
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }
    }
}