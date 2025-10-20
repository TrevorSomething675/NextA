using Nexta.Application.DTO.Product;
using Nexta.Application.Common;
using Nexta.Domain.Base;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQueryResponse : BasePagedResponse<ProductDto>
    {
        public GetProductsQueryResponse(PagedData<ProductDto> data) : base(data) { }
    }
}