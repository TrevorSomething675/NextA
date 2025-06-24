using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
    public class GetAllOrdersQueryRequest : IRequest<GetAllOrdersQueryResponse>
    {
        public GetAllOrdersFilter Filter { get; set; }
    }
}