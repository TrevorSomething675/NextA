using FluentValidation;

namespace Nexta.Application.Queries.CodeQueries.SendVerificationCodeQuery
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