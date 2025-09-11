using Nexta.Domain.Filters.Products;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductsQuery
{
    public class GetProductsQuery : IRequest<GetProductsQueryResponse>
    {
        public GetProductsFilter Filter { get; init; } = null!;
    }
}
