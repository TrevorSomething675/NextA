using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryResponse : BasePagedResponse<OrderResponse>
	{
		public GetAdminOrdersQueryResponse(PagedData<OrderResponse> data) : base(data) { }
	}
}