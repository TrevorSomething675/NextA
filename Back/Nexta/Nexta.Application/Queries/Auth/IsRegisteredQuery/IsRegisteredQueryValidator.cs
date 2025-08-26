using FluentValidation;

namespace Nexta.Application.Queries.Auth.IsRegisteredQuery
{
    public class IsRegisteredQueryValidator : AbstractValidator<IsRegisteredQuery>
    {
        public IsRegisteredQueryValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Неверная почта");
        }
    }
}