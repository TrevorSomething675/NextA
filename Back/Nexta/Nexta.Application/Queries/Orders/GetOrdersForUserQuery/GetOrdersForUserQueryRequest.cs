using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQueryRequest : IRequest<GetOrdersForUserQueryResponse>
    {
        public GetOrdersFilter Filter { get; init; } = null!;
    }
}