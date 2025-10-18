using Nexta.Application.DTO.Basket;
using MediatR;

namespace Nexta.Application.Commands.Basket.UpdateBasketProductCommand
{
    public class UpdateBasketProductCommand : IRequest<BasketItemDto>
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public DateOnly? DeliveryDate { get; init; }
        public int Count { get; init; }
    }
}