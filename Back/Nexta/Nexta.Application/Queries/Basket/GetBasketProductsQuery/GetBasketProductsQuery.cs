using Nexta.Application.DTO.Basket;
using MediatR;

namespace Nexta.Application.Queries.Basket.GetBasketProductsQuery
{
    public class GetBasketProductsQuery(Guid userId) : IRequest<BasketDto>
    {
        public Guid UserId { get; init; } = userId;
    }
}