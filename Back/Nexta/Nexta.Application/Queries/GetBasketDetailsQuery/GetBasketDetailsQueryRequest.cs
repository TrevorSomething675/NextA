using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetUserBasketDetails
{
    public class GetBasketDetailsQueryRequest : IRequest<GetBasketDetailsQueryResponse>
    {
		public GetBasketDetailsFilter Filter { get; set; } = null!;
    }
}