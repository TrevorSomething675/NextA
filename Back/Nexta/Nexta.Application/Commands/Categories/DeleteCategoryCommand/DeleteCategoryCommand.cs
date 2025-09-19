using MediatR;

namespace Nexta.Application.Commands.Categories.DeleteCategoryCommand
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }
}
