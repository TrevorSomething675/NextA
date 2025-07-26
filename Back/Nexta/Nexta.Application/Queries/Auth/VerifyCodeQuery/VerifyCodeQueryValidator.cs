using FluentValidation;

namespace Nexta.Application.Queries.Auth.VerifyCodeQuery
{
    public class VerifyCodeQueryValidator : AbstractValidator<VerifyCodeQueryRequest>
    {
        public VerifyCodeQueryValidator() 
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