using FluentValidation;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryValidator : AbstractValidator<IsRegisteredQueryRequest>
    {
        public IsRegisteredQueryValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty().NotNull()
                .WithMessage("Неверная почта");
        }
    }
}