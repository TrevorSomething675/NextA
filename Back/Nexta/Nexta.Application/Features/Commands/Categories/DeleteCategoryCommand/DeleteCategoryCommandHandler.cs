using MediatR;
using Nexta.Application.Abstractions;

namespace Nexta.Application.Commands.Categories.DeleteCategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken ct)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(command.Id, ct);
            var deletedCategory = _unitOfWork.Categories.Delete(category, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}