using Nexta.Domain.Models.Images;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IDetailImageRepository
    {
        Task<DetailImage> UpdateAsync(DetailImage detailImage, CancellationToken ct = default);
        Task<Guid> RemoveAsync(Guid id, CancellationToken ct = default);
        Task<Guid> AddAsync(DetailImage detailImage, CancellationToken ct = default);
    }
}
