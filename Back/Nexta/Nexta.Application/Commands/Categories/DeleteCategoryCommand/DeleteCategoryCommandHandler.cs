using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Commands.Categories.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public DeleteCategoryCommandHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken ct)
        {
            var deletedCategory = await _categoriesRepository.DeleteAsync(command.Name, ct);

            return Unit.Value;
        }
    }
}
