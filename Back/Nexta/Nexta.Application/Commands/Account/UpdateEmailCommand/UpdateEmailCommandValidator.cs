using FluentValidation;

namespace Nexta.Application.Commands.Account.UpdateEmailCommand
{
    public class UpdateEmailCommandValidator : AbstractValidator<UpdateEmailCommand>
    {
        public UpdateEmailCommandValidator()
        {
            RuleFor(r => r.Code)
                .NotEmpty().NotNull();
            
            RuleFor(r => r.Email)
                .NotEmpty().NotNull().EmailAddress();

            RuleFor(r => r.LegacyEmail)
                .NotEmpty().NotNull().EmailAddress();
        }
    }
}