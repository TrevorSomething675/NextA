using Nexta.Application.DTO;

namespace Nexta.Application.Queries.Basket.GetBasketDetailsQuery
{
    public class GetBasketDetailsQueryResponse(List<DetailResponse> details)
    {
		public List<DetailResponse>? Details { get; init; } = details;
	}
}