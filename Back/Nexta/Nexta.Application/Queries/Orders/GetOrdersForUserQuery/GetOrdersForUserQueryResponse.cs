using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO.Response;

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