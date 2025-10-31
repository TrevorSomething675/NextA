using FluentValidation;

namespace Nexta.Application.Commands.Auth.SendVerifyCodeCommand
{
    public class SendVerifyCodeCommandValidator : AbstractValidator<SendVerifyCodeCommand>
    {
        public SendVerifyCodeCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Почта не должна быть пустой")
                .EmailAddress()
                .WithMessage("Неверный Email");
        }
    }
}
