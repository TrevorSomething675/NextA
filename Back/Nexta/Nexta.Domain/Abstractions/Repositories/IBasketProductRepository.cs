using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IBasketProductRepository
    {
        Task<BasketProduct?> GetAsync(Guid userId, Guid productId, CancellationToken ct = default);
        Task<List<BasketProduct>?> GetRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default);
        Task<BasketProduct> AddAsync(BasketProduct basketProductToAdd, CancellationToken ct = default);
        Task<BasketProduct> DeleteAsync(BasketProduct basketProductToAdd, CancellationToken ct = default);
        Task<List<BasketProduct>> DeleteRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default);
        Task<BasketProduct> UpdateAsync(BasketProduct basketProductToUpdate, CancellationToken ct = default);
    }
}
