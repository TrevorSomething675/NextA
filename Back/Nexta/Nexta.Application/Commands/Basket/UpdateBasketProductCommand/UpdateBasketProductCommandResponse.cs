namespace Nexta.Application.Commands.Basket.UpdateBasketProductCommand
{
    public class UpdateBasketProductCommandResponse
    {
		public Guid UserId { get; init; }
		public Guid ProductId { get; init; }
		public DateOnly DeliveryDate { get; set; }
		public int Count { get; set; }
	}
}