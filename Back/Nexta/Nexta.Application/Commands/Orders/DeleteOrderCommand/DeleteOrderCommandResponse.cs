namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
    public class DeleteOrderCommandResponse(Guid orderId)
    {
        public Guid OrderId { get; init; } = orderId;
    }
}