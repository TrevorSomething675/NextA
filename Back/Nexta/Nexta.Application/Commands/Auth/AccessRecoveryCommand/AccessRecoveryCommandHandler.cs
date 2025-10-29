using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Abstractions;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Constants;
using FluentValidation;
using MediatR;

namespace Nexta.Application.Commands.Auth.AccessRecoveryCommand
{
    public class AccessRecoveryCommandHandler : IRequestHandler<AccessRecoveryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashService _passwordHashService;
        private readonly IEmailService _emailService;
        private readonly IValidator<AccessRecoveryCommand> _validator;
        private readonly IVerificationCodeService _verificationCodeService;

        public AccessRecoveryCommandHandler(IUnitOfWork unitOfWork, IVerificationCodeService verificationCodeService
            , IValidator<AccessRecoveryCommand> validator, IHashService passwordHashService, IEmailService emailService)
        {
            _verificationCodeService = verificationCodeService;
            _passwordHashService = passwordHashService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Unit> Handle(AccessRecoveryCommand command, CancellationToken ct)
        {
            var validationResult = await _validator.ValidateAsync(command, ct);

            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(", ", validationResult.Errors));

            var verifyResult = _verificationCodeService.VerifyCode(command.Email, command.Code);
            if (!verifyResult)
                throw new BadRequestException("Неверный код");

            var user = await _unitOfWork.Users.GetByEmailAsync(command.Email, ct);

            if (user == null)
                throw new NotFoundException("Пользователь не зарегистрирован");

            var passwordHash = _passwordHashService.Generate(command.Password);

            user.ChangePassword(passwordHash);
            user.AddNotification(
                    "Пароль бы успешно обновлён.",
                    NotificationKeys.WarningScamAccessRecovery);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync(ct);
            await _emailService.SendEmailAsync(user.Email!, "", "Пароль бы успешно обновлён.", NotificationKeys.WarningScamAccessRecovery, ct);

            return Unit.Value;
        }
    }
}
