using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Commands.Users.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUsersRepository _usersRepository;
        
        public DeleteUserCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken ct)
        {
            var result = await _usersRepository.DeleteAsync(command.UserId, ct);

            return Unit.Value;
        }
    }
}
