using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateOrderDetailCommand
{
    public class UpdateOrderDetailCommandRequest : IRequest<UpdateOrderDetailCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid DetailId { get; init; }
        public int Count { get; init; }
    }
}