using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using MediatR;

namespace Nexta.Application.Commands.Account.ConfirmPhoneCommand
{
    public class ConfirmPhoneCommandHandler : IRequestHandler<ConfirmPhoneCommand, ConfirmPhoneCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public ConfirmPhoneCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ConfirmPhoneCommandResponse> Handle(ConfirmPhoneCommand command, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, ct);

            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            user.Phone = command.Phone;
            var updatedUser = await _userRepository.UpdateAsync(user, ct);

            if (updatedUser?.Phone == null)
                throw new BadRequestException("Не удалось обновить номер");

            return new ConfirmPhoneCommandResponse(updatedUser.Phone);
        }
    }
}