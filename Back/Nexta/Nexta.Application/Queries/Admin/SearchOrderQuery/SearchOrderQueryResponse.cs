using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Admin.SearchOrderQuery
{
    public class SearchOrderQueryResponse : BasePagedResponse<OrderDetailResponse>
    {
        public SearchOrderQueryResponse(PagedData<OrderDetailResponse> data) : base(data) { }
    }
}