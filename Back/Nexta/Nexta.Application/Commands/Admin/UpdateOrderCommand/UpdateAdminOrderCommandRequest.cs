using Nexta.Domain.Enums;
using MediatR;
using Nexta.Application.DTO.Request;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
    public class UpdateAdminOrderCommandRequest : IRequest<UpdateAdminOrderCommandResponse>
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public List<OrderDetailsRequest>? OrderDetails { get; init; }
    }
}