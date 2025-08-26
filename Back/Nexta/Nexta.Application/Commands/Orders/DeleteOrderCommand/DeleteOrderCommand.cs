using MediatR;

namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
    public class DeleteOrderCommand(Guid orderId) : IRequest<DeleteOrderCommandResponse>
    {
        public Guid OrderId { get; init; } = orderId;
    }
}