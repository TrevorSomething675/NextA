using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Details.SearchDetailQuery
{
	public class SearchDetailQueryResponse : BasePagedResponse<DetailResponse>
	{
		public SearchDetailQueryResponse(PagedData<DetailResponse> data) : base(data) { }
	}
}