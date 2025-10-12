using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Account.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IValidator<ChangePasswordCommand> _validator;
        private readonly IUsersRepository _usersRepository;
        private readonly IHashService _hashService;

        public ChangePasswordCommandHandler(IHashService hashService,
            IValidator<ChangePasswordCommand> validator, IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
            _hashService = hashService;
            _validator = validator;
        }

        public async Task<Unit> Handle(ChangePasswordCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if(!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var user = await _usersRepository.GetAsync(command.UserId, ct);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            if (!_hashService.Validate(command.LegacyPassword, user.PasswordHash))
                throw new BadRequestException("Неверный пароль");

            var newPasswordHash = _hashService.Generate(command.Password);
            user.PasswordHash = newPasswordHash;

            var result = await _usersRepository.UpdateAsync(user, ct);

            if(result == null)
                throw new BadRequestException("Не удалось обновить пользователя");

            return Unit.Value;
        }
    }
}