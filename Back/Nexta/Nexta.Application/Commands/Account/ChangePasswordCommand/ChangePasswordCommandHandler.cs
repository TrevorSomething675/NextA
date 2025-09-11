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
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public ChangePasswordCommandHandler(IHashService hashService, 
            IValidator<ChangePasswordCommand> validator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _validator = validator;
        }

        public async Task<Unit> Handle(ChangePasswordCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if(!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var user = await _userRepository.GetAsync(command.UserId, ct);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            if (!_hashService.Validate(command.LegacyPassword, user.PasswordHash))
                throw new BadRequestException("Неверный пароль");

            var newPasswordHash = _hashService.Generate(command.Password);
            user.PasswordHash = newPasswordHash;

            var result = await _userRepository.UpdateAsync(user, ct);

            if(command == null)
                throw new BadRequestException("Не удалось обновить пользователя");

            return Unit.Value;
        }
    }
}