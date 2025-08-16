using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Details.GetDetailsQuery
{
	public class GetDetailsQueryResponse : BasePagedResponse<DetailResponse>
	{
		public GetDetailsQueryResponse(PagedData<DetailResponse> data) : base(data) { }
	}
}