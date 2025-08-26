using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommand : IRequest<AddAdminProductToOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
    }
}