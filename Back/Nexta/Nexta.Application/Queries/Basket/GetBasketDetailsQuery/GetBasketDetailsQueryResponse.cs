using Nexta.Domain.Models;

namespace Nexta.Application.Queries.Basket.GetBasketDetailsQuery
{
    public class GetBasketDetailsQueryResponse(List<Detail> details)
    {
		public List<Detail> Details { get; set; } = details;
	}
}