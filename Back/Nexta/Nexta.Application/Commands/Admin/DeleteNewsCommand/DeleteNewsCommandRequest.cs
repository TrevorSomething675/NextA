using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteNewsCommand
{
    public class DeleteNewsCommandRequest(Guid id) : IRequest<Guid>
    {
        public Guid Id { get; init; } = id;
    }
}