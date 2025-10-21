using Nexta.Application.DTO.Order;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQueryResponse(PagedData<OrderDto> data, int totalCount)
    {
        public PagedData<OrderDto> Data { get; set; } = data;
        public int? TotalCount { get; init; } = totalCount;
    }
}