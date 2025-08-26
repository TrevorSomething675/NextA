using FluentValidation;

namespace Nexta.Application.Commands.Auth.CheckAuthCommand
{
    public class CheckAuthCommandValidator : AbstractValidator<CheckAuthCommand>
    {
        public CheckAuthCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Неверный Email");
        }
    }
}