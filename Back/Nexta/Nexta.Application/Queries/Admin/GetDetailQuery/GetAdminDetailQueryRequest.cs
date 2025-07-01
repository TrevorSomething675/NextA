using MediatR;

namespace Nexta.Application.Queries.Admin.GetDetailQuery
{
    public class GetAdminDetailQueryRequest : IRequest<GetAdminDetailQueryResponse>
    {
        public Guid DetailId { get; set; }
    }
}