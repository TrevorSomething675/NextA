using FluentValidation;

namespace Nexta.Application.Commands.Auth.AccessRecoveryCommand
{
    public class AccessRecoveryCommandValidator : AbstractValidator<AccessRecoveryCommand>
    {
        public AccessRecoveryCommandValidator()
        {
            RuleFor(r => r.Password)
                    .Equal(r => r.ConfirmPassword)
                    .WithMessage("Пароли не совпадают");

            RuleFor(r => r.Email)
                    .MaximumLength(48)
                    .WithMessage("Почта слишком длинная");

            RuleFor(r => r.Password)
                    .MinimumLength(6)
                    .WithMessage("Пароль слишком короткий")
                    .MaximumLength(64)
                    .WithMessage("Пароль слишком длинный");
        }
    }
}
