using FluentValidation;

namespace Nexta.Application.Commands.Categories.AddCategoryCommand
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().NotNull();
        }
    }
}