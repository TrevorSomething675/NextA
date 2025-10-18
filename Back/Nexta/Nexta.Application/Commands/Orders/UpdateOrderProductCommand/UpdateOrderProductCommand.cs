using MediatR;
using Nexta.Application.DTO.Order;

namespace Nexta.Application.Commands.Orders.UpdateOrderProductCommand
{
    public class UpdateOrderProductCommand : IRequest<OrderItemDto>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}