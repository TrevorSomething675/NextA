using FluentValidation;

namespace Nexta.Application.Queries.Auth.SendVerificationCodeQuery
{
    public class SendVerificationCodeQueryValidator : AbstractValidator<SendVerificationCodeQueryRequest>
    {
        public SendVerificationCodeQueryValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty().NotNull()
                .EmailAddress()
                .WithMessage("Неверный Email");
        }
    }
}