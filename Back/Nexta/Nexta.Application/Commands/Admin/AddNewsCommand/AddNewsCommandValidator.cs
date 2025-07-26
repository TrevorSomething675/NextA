using FluentValidation;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommandValidator : AbstractValidator<AddNewsCommandRequest>
    {
        public AddNewsCommandValidator()
        {
            RuleFor(r => r.Image)
                .NotEmpty()
                .WithMessage("Новость должна содержать картинку");

            RuleFor(r => r.Image)
                .NotEmpty()
                .When(x => x.Image != null)
                .ChildRules(image =>
                {
                    image.RuleFor(i => i.Name).NotEmpty()
                        .WithMessage("Имя картинки для новости отсутствует");

                    image.RuleFor(i => i.Base64String).NotEmpty()
                        .WithMessage("Полезная нагрузка картинки для новости отсутствует");
                });
        }
    }
}