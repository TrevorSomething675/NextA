namespace Nexta.Application.Commands.Basket.DeleteBasketProductCommand
{
    public class DeleteBasketProductCommandResponse(Guid userId, Guid productId)
    {
		public Guid UserId { get; set; } = userId;
		public Guid ProductId { get; set; } = productId;
	}
}