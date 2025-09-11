using FluentValidation;

namespace Nexta.Application.Commands.Orders.CreateNewOrderCommand
{
    public class CreateNewOrderCommandValidator : AbstractValidator<CreateNewOrderCommand>
    {
        public CreateNewOrderCommandValidator() 
        {
            RuleFor(r => r.ProductIds)
                .NotEmpty()
                .WithMessage("В корзине нет позиций");

			RuleFor(r => r.UserId)
                .NotEmpty()
                .WithMessage("В корзине нет позиций");
        }
    }
}