using MediatR;

namespace Nexta.Application.Commands.Users.DeleteUserCommand
{
    public class DeleteUserCommand(Guid userId) : IRequest<Unit>
    {
        public Guid UserId { get; set; } = userId;
    }
}
