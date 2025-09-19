using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Account.UpdateEmailCommand
{
    public class UpdateEmailCommandHandler : IRequestHandler<UpdateEmailCommand, UpdateEmailCommandResponse>
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IValidator<UpdateEmailCommand> _validator;
        private readonly IUsersRepository _usersRepository;

        public UpdateEmailCommandHandler(IUsersRepository usersRepository, 
            IVerificationCodeService verificationCodeService, IValidator<UpdateEmailCommand> validator)
        {
            _verificationCodeService = verificationCodeService;
            _usersRepository = usersRepository;
            _validator = validator;
        }

        public async Task<UpdateEmailCommandResponse> Handle(UpdateEmailCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(',', validationResult.Errors));

            var verifyResult = _verificationCodeService.VerifyCode(command.Email, command.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var dbUser = await _usersRepository.GetByEmailAsync(command.LegacyEmail, ct);
            if (dbUser == null)
                throw new NotFoundException("Пользователь не зарегистрирован");

            if(dbUser.Email != command.LegacyEmail)
                throw new NotFoundException("Ошибка валидации"); // сделано специально

            dbUser.Email = command.Email;

            var updateUser = await _usersRepository.UpdateAsync(dbUser, ct);

            return new UpdateEmailCommandResponse(updateUser);
        }
    }
}
