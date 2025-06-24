using MediatR;

namespace Nexta.Application.Queries.CodeQueries.VerifyCodeQuery
{
    public class VerifyCodeQueryRequest : IRequest<VerifyCodeQueryResponse>
    {
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }
    }
}