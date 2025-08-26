using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
    public class GetBasketProductsQueryResponse(List<ProductResponse> products)
    {
		public List<ProductResponse>? Products { get; init; } = products;
	}
}