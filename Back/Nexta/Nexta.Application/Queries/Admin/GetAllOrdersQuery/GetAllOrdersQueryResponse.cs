using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAllOrdersQueryResponse : BasePagedResponse<OrderResponse>
	{
		public GetAllOrdersQueryResponse(PagedData<OrderResponse> data) : base(data) { }
	}
}