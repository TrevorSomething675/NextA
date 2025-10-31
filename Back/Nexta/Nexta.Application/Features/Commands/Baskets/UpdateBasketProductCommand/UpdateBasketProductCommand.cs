using Nexta.Application.DTO.Baskets;
using MediatR;

namespace Nexta.Application.Commands.Baskets.UpdateBasketProductCommand
{
    public class UpdateBasketProductCommand : IRequest<BasketItemDto>
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public DateOnly? DeliveryDate { get; init; }
        public int Count { get; init; }
    }
}