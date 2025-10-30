using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Baskets;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Basket?> GetByUserIdAsync(ISpecification<Basket> spec, CancellationToken ct = default);
        Task<Basket> AddAsync(Basket basket, CancellationToken ct = default);
        Basket Update(Basket basket);
    }
}