using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetOrdersQuery
{
    public class GetOrdersForUserQueryRequest : IRequest<GetOrdersForUserQueryResponse>
    {
        public GetOrdersFilter Filter { get; set; }
    }
}