using Nexta.Application.DTO.Request;
using Nexta.Domain.Enums;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
    public class UpdateAdminOrderCommand : IRequest<UpdateAdminOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public List<OrderProductsRequest>? OrderProducts { get; init; }
    }
}