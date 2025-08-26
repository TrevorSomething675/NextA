using Nexta.Application.DTO.Response;
using Nexta.Domain.Models.DataModels;
using Nexta.Application.Common;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryResponse : BasePagedResponse<ProductResponse>
    {
        public GetProductsQueryResponse(PagedData<ProductResponse> data) : base(data) { }
    }
}
