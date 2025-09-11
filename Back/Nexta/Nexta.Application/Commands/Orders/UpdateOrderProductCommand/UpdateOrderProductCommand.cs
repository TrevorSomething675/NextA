using MediatR;

namespace Nexta.Application.Commands.Orders.UpdateOrderProductCommand
{
    public class UpdateOrderProductCommand : IRequest<UpdateOrderProductCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}