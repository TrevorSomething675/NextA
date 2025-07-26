using Nexta.Domain.Abstractions.Repositories;
using MediatR;

namespace Nexta.Application.Commands.Admin.DeleteNewsCommand
{
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommandRequest, DeleteNewsCommandResponse>
    {
        private readonly INewsRepository _newsRepository;

        public DeleteNewsCommandHandler(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<DeleteNewsCommandResponse> Handle(DeleteNewsCommandRequest request, CancellationToken ct = default)
        {
            var deletedNewsId = await _newsRepository.DeleteAsync(request.Id, ct);

            return new DeleteNewsCommandResponse(deletedNewsId);
        }
    }
}