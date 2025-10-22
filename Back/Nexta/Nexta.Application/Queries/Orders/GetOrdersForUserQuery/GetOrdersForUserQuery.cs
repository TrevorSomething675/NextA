using Nexta.Application.DTO.Order;
using Nexta.Domain.Filters;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQuery : IRequest<PagedData<OrderDto>>
    {
        public GetOrdersFilter Filter { get; init; } = null!;
    }
}