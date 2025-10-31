using MediatR;

namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
    public class DeleteOrderCommand(Guid orderId) : IRequest<Guid>
    {
        public Guid OrderId { get; init; } = orderId;
    }
}