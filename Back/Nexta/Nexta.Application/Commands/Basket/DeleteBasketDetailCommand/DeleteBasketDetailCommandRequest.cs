using MediatR;

namespace Nexta.Application.Commands.Basket.DeleteBasketDetailCommand
{
    public class DeleteBasketDetailCommandRequest : IRequest<DeleteBasketDetailCommandResponse>
	{
		public Guid UserId { get; set; }
		public Guid DetailId { get; set; }
	}
}
