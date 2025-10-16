using Nexta.Application.Common;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryResponse : BasePagedResponse<OrderResponse>
	{
		public GetAdminOrdersQueryResponse(PagedData<OrderResponse> data) : base(data) { }
	}
}