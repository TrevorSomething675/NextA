using Nexta.Application.DTO.Products;
using Nexta.Domain.Enums;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderCommand
{
    public class UpdateAdminOrderCommand : IRequest<Guid>
    {
        public Guid OrderId { get; init; }
        public Guid UserId { get; init; }
        public OrderStatus Status { get; init; }
        public List<ProductDto>? OrderProducts { get; init; }
    }
}