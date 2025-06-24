using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQueryResponse(PagedData<Order> orders, int totalCount)
    {
        public PagedData<Order> Orders { get; set; } = orders;
        public int TotalCount { get; set; } = totalCount;
    }
}