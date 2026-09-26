using FluentValidation;

namespace Nexta.Application.Queries.Auth.IsRegistrationQuery
{
    public class IsRegistrationValidator : AbstractValidator<IsRegistrationQuery>
    {
        public IsRegistrationValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Неверная почта");
        }
    }
}