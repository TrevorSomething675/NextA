using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommand : IRequest<CreateNewOrderCommandResponse>
    {
        public Guid UserId { get; set; }
        public List<Guid> ProductIds { get; set; }
        public DateOnly CreatedDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    }
}