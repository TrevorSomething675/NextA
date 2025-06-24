using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
    public class GetLegacyOrdersQueryResponse(PagedData<Order> orders)
    {
		public PagedData<Order> Orders { get; set; } = orders;
	}
}