using Nexta.Application.DTO.Product;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQueryResponse(ProductDto product)
    {
        public ProductDto Product { get; init; } = product;
    }
}
