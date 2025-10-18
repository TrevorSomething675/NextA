using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Commands.Account.ConfirmPhoneCommand
{
    public class ConfirmPhoneCommandHandler : IRequestHandler<ConfirmPhoneCommand, ConfirmPhoneCommandResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public ConfirmPhoneCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ConfirmPhoneCommandResponse> Handle(ConfirmPhoneCommand command, CancellationToken ct = default)
        {
            var user = await _usersRepository.GetByEmailAsync(command.Email, ct);

            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            user.ChangePhone(command.Phone);
            var updatedUser = await _usersRepository.UpdateAsync(user, ct);

            if (updatedUser?.Phone == null)
                throw new BadRequestException("Не удалось обновить номер");

            return new ConfirmPhoneCommandResponse(updatedUser.Phone);
        }
    }
}