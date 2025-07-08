using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteDetailFromOrderCommand
{
    public class DeleteDetailFromOrderCommandRequest : IRequest<DeleteDetailFromOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid DetailId { get; init; }
    }
}