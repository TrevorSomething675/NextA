using Nexta.Application.DTO.Admin;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
	public class GetAdminProductsQueryResponse : BasePagedResponse<AdminProductResponse>
	{
		public GetAdminProductsQueryResponse(PagedData<AdminProductResponse> data) : base(data) { }
	}
}