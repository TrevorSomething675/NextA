using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
    public class DeleteProductFromOrderCommand : IRequest<DeleteProductFromOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
    }
}