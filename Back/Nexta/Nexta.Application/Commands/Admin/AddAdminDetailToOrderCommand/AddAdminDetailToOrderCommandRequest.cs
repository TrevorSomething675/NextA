using MediatR;

namespace Nexta.Application.Commands.Admin.AddAdminDetailToOrderCommand
{
    public class AddAdminDetailToOrderCommandRequest : IRequest<AddAdminDetailToOrderCommandResponse>
    {
        public Guid OrderId { get; init; }
        public Guid DetailId { get; init; }
        public int Count { get; init; }
    }
}
