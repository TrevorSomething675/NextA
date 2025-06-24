using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
    public class GetAllOrdersQueryResponse(PagedData<Order> orders)
    {
        public PagedData<Order> Orders { get; set; } = orders;
    }
}