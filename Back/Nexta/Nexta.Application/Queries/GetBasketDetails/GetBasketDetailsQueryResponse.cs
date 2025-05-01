using Nexta.Domain.Models;

namespace Nexta.Application.Queries.GetUserBasketDetails
{
    public class GetBasketDetailsQueryResponse(List<Detail> details)
    {
		public List<Detail> Details { get; set; } = details;
	}
}