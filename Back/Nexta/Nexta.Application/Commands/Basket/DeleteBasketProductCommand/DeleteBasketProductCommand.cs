using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketProductCommand
{
    public class DeleteBasketProductCommand : IRequest<Guid>
	{
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
	}
}
