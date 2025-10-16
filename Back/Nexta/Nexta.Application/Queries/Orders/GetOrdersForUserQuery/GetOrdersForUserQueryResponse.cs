using Nexta.Application.DTO.Response;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQueryResponse : BasePagedResponse<OrderResponse>
    {
		public GetOrdersForUserQueryResponse(PagedData<OrderResponse> data, int totalCount) : base(data)
		{
			TotalCount = totalCount;
		}

		public int? TotalCount { get; init; }
    }
}