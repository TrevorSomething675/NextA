using FluentValidation;

namespace Nexta.Application.Commands.Admin.DeleteProductFromOrderCommand
{
    public class DeleteProductFromOrderCommandValidator : AbstractValidator<DeleteProductFromOrderCommand>
    {
        public DeleteProductFromOrderCommandValidator()
        {
            RuleFor(r => r.ProductId)
                .NotEmpty()
                .WithMessage("Id позиции не должен быть пустым");

            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage("Id заказа не должен быть пустым");
        }
    }
}
