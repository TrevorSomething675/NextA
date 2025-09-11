using Nexta.Domain.Models.DataModels;
using Nexta.Application.DTO.Admin;
using Nexta.Application.Common;

namespace Nexta.Application.Queries.Admin.GetProductsQuery
{
	public class GetAdminProductsQueryResponse : BasePagedResponse<AdminProductResponse>
	{
		public GetAdminProductsQueryResponse(PagedData<AdminProductResponse> data) : base(data) { }
	}
}