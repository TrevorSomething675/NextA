using FluentValidation;

namespace Nexta.Application.Commands.Auth.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Неверная почта");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Неверный пароль");
        }
    }
}