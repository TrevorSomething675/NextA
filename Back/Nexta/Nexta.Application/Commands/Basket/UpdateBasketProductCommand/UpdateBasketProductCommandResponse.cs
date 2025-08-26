namespace Nexta.Application.Commands.Basket.UpdateBasketProductCommand
{
    public class UpdateBasketProductCommandResponse
    {
		public Guid OrderId { get; init; }
		public Guid DetailId { get; init; }
		public DateOnly DeliveryDate { get; set; }
		public int Count { get; set; }
	}
}