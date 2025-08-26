namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
    public class DeleteProductFromOrderCommandResponse(Guid orderId, Guid productId)
    {
        public Guid OrderId { get; init; } = orderId;
        public Guid ProductId { get; init; } = productId;
    }
}