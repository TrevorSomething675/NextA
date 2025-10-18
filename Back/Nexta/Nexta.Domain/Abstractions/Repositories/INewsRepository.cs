using Nexta.Domain.Models.News;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync(CancellationToken ct = default);
        Task<News> GetByIdAsync(Guid id,  CancellationToken ct = default);

        Task<News> AddAsync(News news, CancellationToken ct = default);
        Guid Update(News news, CancellationToken ct = default);
        Guid DeleteAsync(News news, CancellationToken ct = default);
    }
}