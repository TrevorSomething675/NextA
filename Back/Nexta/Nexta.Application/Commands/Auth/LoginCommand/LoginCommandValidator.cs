using FluentValidation;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotNull().NotEmpty()
                .WithMessage("Неверная почта");

            RuleFor(r => r.Password)
                .NotNull().NotEmpty()
                .WithMessage("Неверный пароль");
        }
    }
}