namespace Nexta.Application.Commands.Basket.DeleteBasketDetailCommand
{
    public class DeleteBasketDetailCommandResponse(Guid userId, Guid detailId)
    {
		public Guid UserId { get; set; } = userId;
		public Guid DetailId { get; set; } = detailId;
	}
}