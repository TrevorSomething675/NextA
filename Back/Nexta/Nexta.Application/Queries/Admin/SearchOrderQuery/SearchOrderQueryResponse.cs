using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;
using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Admin.SearchOrderQuery
{
    public class SearchOrderQueryResponse : BasePagedResponse<OrderProductResponse>
    {
        public SearchOrderQueryResponse(PagedData<OrderProductResponse> data) : base(data) { }
    }
}