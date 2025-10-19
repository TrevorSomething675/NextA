using Nexta.Application.DTO.Order;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQueryResponse : BasePagedResponse<OrderDto>
    {
		public GetOrdersForUserQueryResponse(PagedData<OrderDto> data, int totalCount) : base(data)
		{
			TotalCount = totalCount;
		}

		public int? TotalCount { get; init; }
    }
}