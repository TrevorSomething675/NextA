using FluentValidation;

namespace Nexta.Application.Commands.Auth.VerifyCodeQuery
{
    public class VerifyCodeCommandValidator : AbstractValidator<VerifyCodeCommand>
    {
        public VerifyCodeCommandValidator() 
        {
            RuleFor(r => r.Code)
                .NotEmpty()
                .WithMessage("Пустой код");

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Неверный Email");
		}
    }
}