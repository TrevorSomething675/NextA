namespace Nexta.Application.Commands.Categories.AddCategoryCommand
{
    public class AddCategoryCommandResponse(Guid id)
    {
        public Guid Id { get; init; } = id;
    }
}
