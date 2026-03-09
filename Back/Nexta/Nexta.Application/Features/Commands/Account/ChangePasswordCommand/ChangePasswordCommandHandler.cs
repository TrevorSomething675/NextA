using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Commands.Account.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IValidator<ChangePasswordCommand> _validator;
        private readonly IHashService _hashService;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(IHashService hashService,
            IValidator<ChangePasswordCommand> validator, IUnitOfWork unitOfWork)
        {
            _hashService = hashService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(ChangePasswordCommand command, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if(!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var user = await _unitOfWork.Users.GetByIdAsync(command.UserId, ct);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            if (!_hashService.Validate(command.LegacyPassword, user.PasswordHash))
                throw new BadRequestException("Неверный пароль");

            var passwordHash = _hashService.Generate(command.Password);
            user.ChangePassword(passwordHash);

            var result = _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);

            if(result == null)
                throw new BadRequestException("Не удалось обновить пользователя");

            return Unit.Value;
        }
    }
}