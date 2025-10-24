using Nexta.Application.DTO.Order;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
    public class DeleteProductFromOrderCommand : IRequest<OrderItemDto>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
    }
}