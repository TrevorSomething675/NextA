using FluentValidation;

namespace Nexta.Application.Commands.Account.ChangePasswordCommand
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Неверный пользователь.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Неверная почта")
                .EmailAddress().WithMessage("Неверная почта");

            RuleFor(r => r.LegacyPassword)
                .NotEmpty().WithMessage("Пустой пароль");

            RuleFor(r => r.Password)
                .MinimumLength(6)
                .WithMessage("Пароль слишком короткий")
                .MaximumLength(64)
                .WithMessage("Пароль слишком длинный")
                .Equal(r => r.ConfirmPassword)
                .WithMessage("Пароли не совпадают");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Пустое подтверждение пароля");
        }
    }
}