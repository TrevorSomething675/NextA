using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Commands.Account.ConfirmPhoneCommand
{
    public class ConfirmPhoneCommandHandler : IRequestHandler<ConfirmPhoneCommand, ConfirmPhoneCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPhoneCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConfirmPhoneCommandResponse> Handle(ConfirmPhoneCommand command, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(command.Email, ct);

            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            user.ChangePhone(command.Phone);
            var updatedUser = _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);

            if (updatedUser?.Phone == null)
                throw new BadRequestException("Не удалось обновить номер");

            return new ConfirmPhoneCommandResponse(updatedUser.Phone);
        }
    }
}