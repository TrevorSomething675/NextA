using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketDetailsQuery
{
    public class GetBasketDetailsQueryRequest : IRequest<GetBasketDetailsQueryResponse>
    {
		public GetBasketDetailsFilter Filter { get; init; } = null!;
    }
}