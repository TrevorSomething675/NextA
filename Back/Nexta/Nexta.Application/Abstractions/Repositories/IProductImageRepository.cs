using Nexta.Domain.Models.Images;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductImageRepository
    {
        Task<ProductImage> UpdateAsync(ProductImage productImage, CancellationToken ct = default);
        Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default);
        Task<Guid> AddAsync(ProductImage productImage, CancellationToken ct = default);
    }
}
