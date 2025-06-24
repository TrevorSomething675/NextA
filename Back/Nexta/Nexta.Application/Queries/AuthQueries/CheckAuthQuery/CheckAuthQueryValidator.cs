using FluentValidation;

namespace Nexta.Application.Queries.AuthQueries.CheckAuthQuery
{
    public class CheckAuthQueryValidator : AbstractValidator<CheckAuthQueryRequest>
    {
        public CheckAuthQueryValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty().NotNull()
                .WithMessage("Пустой пользователь");
        }
    }
}