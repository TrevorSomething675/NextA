using MediatR;

namespace Nexta.Application.Commands.Basket.UpdateBasketDetailCommand
{
    public class UpdateBasketDetailCommandRequest : IRequest<UpdateBasketDetailCommandResponse>
    {
        public Guid UserId { get; init; }
        public Guid DetailId { get; init; }
		public DateOnly? DeliveryDate { get; init; }
		public int? Count { get; init; }
	}
}