using Nexta.Application.DTO.Baskets;
using MediatR;

namespace Nexta.Application.Commands.Baskets.AddBasketProductCommand
{
    public class AddBasketProductCommand : IRequest<BasketItemDto>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
