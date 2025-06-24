using MediatR;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommandRequest : IRequest<CreateNewOrderCommandResponse>
    {
        public Guid UserId { get; set; }
        public List<Guid> DetailIds { get; set; }
    }
}