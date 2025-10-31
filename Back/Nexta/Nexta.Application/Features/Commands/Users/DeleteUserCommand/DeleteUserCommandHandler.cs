using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Commands.Users.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken ct)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(command.UserId, ct);
            var result = _unitOfWork.Users.Delete(user);

            await _unitOfWork.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}