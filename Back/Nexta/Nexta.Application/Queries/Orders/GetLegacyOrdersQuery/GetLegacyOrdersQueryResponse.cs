using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
	public class GetLegacyOrdersQueryResponse : BasePagedResponse<OrderResponse>
	{
		public GetLegacyOrdersQueryResponse(PagedData<OrderResponse> data) : base(data) { }
	}
}