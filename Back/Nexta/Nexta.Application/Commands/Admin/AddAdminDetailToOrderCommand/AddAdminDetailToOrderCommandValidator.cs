using FluentValidation;

namespace Nexta.Application.Commands.Admin.AddAdminDetailToOrderCommand
{
    public class AddAdminDetailToOrderCommandValidator : AbstractValidator<AddAdminDetailToOrderCommandRequest>
    {
        public AddAdminDetailToOrderCommandValidator()
        {
            RuleFor(r => r.DetailId)
                .NotEmpty().WithMessage("Id детали пустой");

            RuleFor(r => r.OrderId)
                .NotEmpty().WithMessage("Id пользователя пустой");
        }
    }
}
