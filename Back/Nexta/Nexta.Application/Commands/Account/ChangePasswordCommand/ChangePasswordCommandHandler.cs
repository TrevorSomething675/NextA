using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Nexta.Domain.Abstractions.Repositories;

namespace Nexta.Application.Commands.Account.ChangePasswordCommand
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, Unit>
    {
        private readonly IValidator<ChangePasswordCommandRequest> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public ChangePasswordCommandHandler(IHashService hashService, 
            IValidator<ChangePasswordCommandRequest> validator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _validator = validator;
        }

        public async Task<Unit> Handle(ChangePasswordCommandRequest request, CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(request, ct);

            if(!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var user = await _userRepository.GetAsync(request.UserId, ct);
            if (user == null)
                throw new NotFoundException("Пользователь не найден");

            if (!_hashService.Validate(request.LegacyPassword, user.PasswordHash))
                throw new BadRequestException("Неверный пароль");

            var newPassword = _hashService.Generate(request.Password);
            user.PasswordHash = newPassword;

            var result = await _userRepository.UpdateAsync(user, ct);

            if(request == null)
                throw new BadRequestException("Не удалось обновить пользователя");

            return Unit.Value;
        }
    }
}

//070602