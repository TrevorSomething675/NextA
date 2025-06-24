using MediatR;

namespace Nexta.Application.Queries.AuthQueries.CheckAuthQuery
{
    public class CheckAuthQueryRequest : IRequest<CheckAuthQueryResponse>
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}