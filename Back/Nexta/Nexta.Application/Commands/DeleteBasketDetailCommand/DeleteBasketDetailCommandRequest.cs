using MediatR;

namespace Nexta.Application.Commands.DeleteDetailFromBasket
{
    public class DeleteBasketDetailCommandRequest : IRequest<DeleteBasketDetailCommandResponse>
	{
		public Guid UserId { get; set; }
		public Guid DetailId { get; set; }
	}
}
