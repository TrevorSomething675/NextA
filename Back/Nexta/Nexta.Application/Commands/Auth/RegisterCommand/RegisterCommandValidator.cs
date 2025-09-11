using FluentValidation;

namespace Nexta.Application.Commands.Auth.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email не должен быть пустым");

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

            RuleFor(r => r.FirstName)
				.MaximumLength(32)
                .WithMessage("Слишком длинное имя");

			RuleFor(r => r.LastName)
				.MaximumLength(32)
				.WithMessage("Слишком длинная фамилия");

			RuleFor(r => r.MiddleName)
				.MaximumLength(32)
				.WithMessage("Слишком длинное отчество");
		}
    }
}
