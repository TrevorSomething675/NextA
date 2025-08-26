using Nexta.Domain.Models.Images;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductImageRepository
    {
        Task<ProductImage> UpdateAsync(ProductImage detailImage, CancellationToken ct = default);
        Task<Guid> RemoveAsync(Guid id, CancellationToken ct = default);
        Task<Guid> AddAsync(ProductImage detailImage, CancellationToken ct = default);
    }
}
