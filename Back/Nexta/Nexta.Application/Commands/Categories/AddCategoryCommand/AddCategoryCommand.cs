using MediatR;

namespace Nexta.Application.Commands.Categories.AddCategoryCommand
{
    public class AddCategoryCommand : IRequest<AddCategoryCommandResponse>
    {
        public string Name { get; set; }
    }
}