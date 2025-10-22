using Nexta.Application.DTO.Order;
using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommand : IRequest<CreateNewOrderCommandResponse>
    {
        public Guid UserId { get; set; }
        public List<OrderItemDto> Products { get; set; } = new List<OrderItemDto>();
        public DateOnly CreatedDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    }
}