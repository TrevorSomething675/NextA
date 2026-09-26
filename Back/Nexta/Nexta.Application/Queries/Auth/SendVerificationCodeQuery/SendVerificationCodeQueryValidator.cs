using FluentValidation;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryValidator : AbstractValidator<SendVerificationCodeQuery>
    {
        public SendVerificationCodeQueryValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Почта не должна быть пустой")
                .EmailAddress()
                .WithMessage("Неверный Email");
        }
    }
}