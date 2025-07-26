using FluentValidation;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommandValidator : AbstractValidator<CreateNewOrderCommandRequest>
    {
        public CreateNewOrderCommandValidator() 
        {
            RuleFor(r => r.DetailIds)
                .NotEmpty()
                .WithMessage("В корзине нет позиций");

			RuleFor(r => r.UserId)
                .NotEmpty()
                .WithMessage("В корзине нет позиций");
        }
    }
}