using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Details.GetWarehouseDetailsQuery
{
	public class GetWarehouseDetailsQueryResponse : BasePagedResponse<DetailResponse>
	{
		public GetWarehouseDetailsQueryResponse(PagedData<DetailResponse> data) : base(data) { }
	}
}