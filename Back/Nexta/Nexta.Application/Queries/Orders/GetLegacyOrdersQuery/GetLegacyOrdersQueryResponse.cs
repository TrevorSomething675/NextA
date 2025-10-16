using Nexta.Application.Common;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQueryResponse : BasePagedResponse<OrderResponse>
	{
		public GetLegacyOrdersQueryResponse(PagedData<OrderResponse> data) : base(data) { }
	}
}