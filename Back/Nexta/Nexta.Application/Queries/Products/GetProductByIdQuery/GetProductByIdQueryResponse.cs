using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQueryResponse(ProductResponse product)
    {
        public ProductResponse Product { get; init; } = product;
    }
}
