using Nexta.Domain.Enums;
using MediatR;

namespace Nexta.Application.Commands.Orders.UpdateOrderStatusCommand
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}