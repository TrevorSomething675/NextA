using Nexta.Application.DTO.Order;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
	public class GetAdminOrdersQueryResponse : BasePagedResponse<OrderDto>
	{
		public GetAdminOrdersQueryResponse(PagedData<OrderDto> data) : base(data) { }
	}
}