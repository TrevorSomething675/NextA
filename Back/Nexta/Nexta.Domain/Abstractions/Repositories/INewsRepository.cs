using Nexta.Domain.Models.News;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync(CancellationToken ct = default);
        Task<News> GetByIdAsync(Guid id,  CancellationToken ct = default);

        Task<News> AddAsync(News news, CancellationToken ct = default);
        Task<Guid> UpdateAsync(News newsToUpdate, CancellationToken ct = default);
        Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}