using Nexta.Domain.Models;

namespace Nexta.Application.Abstractions.Repositories
{
    public interface INewsRepository
    {
        Task<List<News>> GetAsync(CancellationToken ct = default);
        Task<News> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<News> AddAsync(News news, CancellationToken ct = default);
        Guid Update(News news, CancellationToken ct = default);
        Guid DeleteAsync(News news, CancellationToken ct = default);
    }
}