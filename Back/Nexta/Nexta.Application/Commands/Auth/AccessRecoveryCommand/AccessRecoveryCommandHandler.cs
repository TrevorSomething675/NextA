using FluentValidation;
using MediatR;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Constants;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;

namespace Nexta.Application.Commands.Auth.AccessRecoveryCommand
{
    public class AccessRecoveryCommandHandler : IRequestHandler<AccessRecoveryCommand, Unit>
    {
        private readonly IHashService _passwordHashService;
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
        private readonly IValidator<AccessRecoveryCommand> _validator;
        private readonly IVerificationCodeService _verificationCodeService;

        public AccessRecoveryCommandHandler(IUsersRepository usersRepository, IVerificationCodeService verificationCodeService
            , IValidator<AccessRecoveryCommand> validator, IHashService passwordHashService, IEmailService emailService)
        {
            _verificationCodeService = verificationCodeService;
            _passwordHashService = passwordHashService;
            _usersRepository = usersRepository;
            _emailService = emailService;
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

            var dbUser = await _usersRepository.GetByEmailAsync(command.Email, ct);

            if (dbUser == null)
                throw new NotFoundException("Пользователь не зарегистрирован");

            var passwordHash = _passwordHashService.Generate(command.Password);

            CreateUserToRegister(ref dbUser, passwordHash!);

            await _usersRepository.UpdateAsync(dbUser, ct);
            await _emailService.SendEmailAsync(dbUser.Email!, "", "Пароль бы успешно обновлён.", NotificationKeys.WarningScamAccessRecovery, ct);

            return Unit.Value;
        }

        private User CreateUserToRegister(ref User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;

            var notification = new Notification
            {
                IsRead = false,
                Header = "Пароль бы успешно обновлён.",
                Message = NotificationKeys.WarningScamAccessRecovery,
                IsTemporary = false,
            };

            user.Notifications.Add(notification);

            return user;
        }
    }
}
