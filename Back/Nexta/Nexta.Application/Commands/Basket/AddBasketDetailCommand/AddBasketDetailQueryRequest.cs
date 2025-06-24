using MediatR;

namespace Nexta.Application.Commands.Basket.AddBasketDetailCommand
{
    public class AddBasketDetailQueryRequest : IRequest<AddBasketDetailQueryResponse>
    {
        public Guid UserId { get; set; }
        public Guid DetailId { get; set; }
        public int CountToPay { get; set; }
    }
}