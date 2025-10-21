using Nexta.Domain.Filters.Products;
using Nexta.Application.DTO.Product;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQuery : IRequest<PagedData<ProductDto>>
    {
        public GetProductsFilter Filter { get; init; } = null!;
    }
}
