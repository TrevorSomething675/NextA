using FluentValidation;

namespace Nexta.Application.Queries.CodeQueries.VerifyCodeQuery
{
    public class VerifyCodeQueryValidator : AbstractValidator<VerifyCodeQueryRequest>
    {
        public VerifyCodeQueryValidator() 
        {
            RuleFor(r => r.Code)
                .NotEmpty().NotNull()
                .WithMessage("Пустой код");

            RuleFor(r => r.Email)
                .NotEmpty().NotNull()
                .EmailAddress()
                .WithMessage("Неверный Email");

            RuleFor(r => r.UserId)
                .NotEmpty().NotNull()
                .WithMessage("Пустой пользователь");
		}
    }
}