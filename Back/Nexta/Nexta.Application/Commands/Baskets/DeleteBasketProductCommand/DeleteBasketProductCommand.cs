using MediatR;

namespace Nexta.Application.Commands.Baskets.DeleteBasketProductCommand
{
    public class DeleteBasketProductCommand : IRequest<Guid>
	{
		public Guid UserId { get; set; }
		public Guid ProductId { get; set; }
	}
}
