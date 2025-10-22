using MediatR;

namespace Nexta.Application.Commands.Categories.AddCategoryCommand
{
    public class AddCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}