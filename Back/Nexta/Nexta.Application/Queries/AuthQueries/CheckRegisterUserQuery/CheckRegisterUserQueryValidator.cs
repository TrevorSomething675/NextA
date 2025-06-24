using FluentValidation;

namespace Nexta.Application.Queries.AuthQueries.CheckRegisterUserQuery
{
    public class CheckRegisterUserQueryValidator : AbstractValidator<CheckRegisterUserQueryRequest>
    {
        public CheckRegisterUserQueryValidator() 
        {
            RuleFor(r => r.Email)
                .NotEmpty().NotNull()
                .WithMessage("Неверная почта");
        }
    }
}