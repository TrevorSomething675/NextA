using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
    public class GetAdminOrdersQueryRequest : IRequest<GetAdminOrdersQueryResponse>
    {
        public GetAllOrdersFilter Filter { get; set; }
    }
}