using FluentValidation;

namespace Nexta.Application.Commands.Admin.DeleteDetailFromOrderCommand
{
    public class DeleteDetailFromOrderCommandValidator : AbstractValidator<DeleteDetailFromOrderCommandRequest>
    {
        public DeleteDetailFromOrderCommandValidator()
        {
            RuleFor(r => r.DetailId)
                .NotEmpty()
                .WithMessage("Id детали не должен быть пустым");

            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage("Id заказа не должен быть пустым");
        }
    }
}
