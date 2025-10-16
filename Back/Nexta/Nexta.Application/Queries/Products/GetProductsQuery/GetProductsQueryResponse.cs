using Nexta.Application.DTO.Response;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryResponse : BasePagedResponse<ProductResponse>
    {
        public GetProductsQueryResponse(PagedData<ProductResponse> data) : base(data) { }
    }
}
