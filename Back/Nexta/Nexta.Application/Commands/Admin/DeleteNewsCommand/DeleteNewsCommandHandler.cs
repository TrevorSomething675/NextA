using Nexta.Domain.Abstractions;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteNewsCommand
{
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommandRequest, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNewsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteNewsCommandRequest request, CancellationToken ct = default)
        {
            var news = await _unitOfWork.News.GetAsync(request.Id, ct);
            var deletedNewsId = _unitOfWork.News.DeleteAsync(news, ct);

            await _unitOfWork.SaveChangesAsync(ct);

            return deletedNewsId;
        }
    }
}