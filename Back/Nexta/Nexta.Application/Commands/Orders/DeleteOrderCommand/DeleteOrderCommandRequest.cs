using MediatR;

namespace Nexta.Application.Commands.Orders.DeleteOrderCommand
{
    public class DeleteOrderCommandRequest : IRequest<DeleteOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
    }
}