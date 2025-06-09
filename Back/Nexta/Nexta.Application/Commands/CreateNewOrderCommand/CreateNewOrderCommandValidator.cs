using FluentValidation;

namespace Nexta.Application.Commands.CreateNewOrderCommand
{
    public class CreateNewOrderCommandValidator : AbstractValidator<CreateNewOrderCommandRequest>
    {
        public CreateNewOrderCommandValidator() 
        {
            RuleFor(r => r.DetailIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("В корзине нет позиций");

			RuleFor(r => r.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("В корзине нет позиций");
        }
    }
}