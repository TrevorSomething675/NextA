using MediatR;

namespace Nexta.Application.Commands.Basket.AddBasketProductCommand
{
    public class AddBasketProductCommand : IRequest<AddBasketProductCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int CountToPay { get; set; }
    }
}
