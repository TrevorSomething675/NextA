using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
    public class GetBasketProductsQuery(Guid userId) : IRequest<GetBasketProductsQueryResponse>
    {
        public Guid UserId { get; init; } = userId;
    }
}