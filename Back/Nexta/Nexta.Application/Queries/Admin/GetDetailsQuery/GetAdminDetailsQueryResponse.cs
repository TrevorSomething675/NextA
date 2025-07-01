using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using Nexta.Application.Common;

namespace Nexta.Application.Queries.Admin.GetDetailsQuery
{
	public class GetAdminDetailsQueryResponse : BasePagedResponse<AdminDetailResponse>
	{
		public GetAdminDetailsQueryResponse(PagedData<AdminDetailResponse> data) : base(data) { }
	}
}