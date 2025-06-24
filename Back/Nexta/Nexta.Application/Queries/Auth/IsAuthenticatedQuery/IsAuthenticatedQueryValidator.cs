using FluentValidation;

namespace Nexta.Application.Queries.Auth.IsAuthenticatedQuery
{
    public class IsAuthenticatedQueryValidator : AbstractValidator<IsAuthenticatedQueryRequest>
    {
        public IsAuthenticatedQueryValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().NotNull()
                .WithMessage("Пустой пользователь");
        }
    }
}