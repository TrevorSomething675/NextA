using Nexta.Domain.Models.Basket;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetAsync(Guid id, CancellationToken ct = default);
        Task<Basket?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
        Basket Update(Basket basket);
    }
}