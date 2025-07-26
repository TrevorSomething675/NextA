using Nexta.Domain.Models.Images;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INewsImageRepository
    {
        Task<Guid> AddAsync(NewsImage imageToAdd, CancellationToken ct = default);
        Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}