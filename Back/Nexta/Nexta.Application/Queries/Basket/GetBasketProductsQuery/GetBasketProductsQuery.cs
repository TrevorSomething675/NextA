using Nexta.Domain.Filters.Products;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
    public class GetBasketProductsQuery : IRequest<GetBasketProductsQueryResponse>
    {
		public GetBasketProductsFilter Filter { get; init; } = null!;
    }
}