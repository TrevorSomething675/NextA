namespace Nexta.Application.Commands.Admin.DeleteDetailFromOrderCommand
{
    public class DeleteDetailFromOrderCommandResponse(Guid orderId, Guid detailId)
    {
        public Guid OrderId { get; init; } = orderId;
        public Guid DetailId { get; init; } = detailId;
    }
}