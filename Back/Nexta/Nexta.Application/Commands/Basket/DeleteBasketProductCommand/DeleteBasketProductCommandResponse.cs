namespace Nexta.Application.Commands.Basket.DeleteBasketProductCommand
{
    public class DeleteBasketProductCommandResponse(Guid basketId)
    {
		public Guid BasketId { get; private set; } = basketId;
	}
}