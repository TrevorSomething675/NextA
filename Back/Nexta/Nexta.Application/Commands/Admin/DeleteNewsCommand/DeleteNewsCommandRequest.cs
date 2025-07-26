using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteNewsCommand
{
    public class DeleteNewsCommandRequest : IRequest<DeleteNewsCommandResponse>
    {
        public Guid Id { get; init; }
    }
}