using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
    public class GetBasketProductsQueryResponse(List<BasketProductResponse> products)
    {
		public List<BasketProductResponse>? Products { get; init; } = products;
	}
}