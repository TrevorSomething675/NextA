using FluentValidation;

namespace Nexta.Application.Commands.Admin.AddAdminProductToOrderCommand
{
    public class AddAdminProductToOrderCommandValidator : AbstractValidator<AddAdminProductToOrderCommand>
    {
        public AddAdminProductToOrderCommandValidator()
        {
            RuleFor(r => r.ProductId)
                .NotEmpty().WithMessage("Id позиции пустой");

            RuleFor(r => r.UserId)
                .NotEmpty().WithMessage("Id пользователя пустой");
        }
    }
}
