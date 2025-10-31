using Nexta.Application.Abstractions.Specification;
using Nexta.Domain.Models.Baskets;

namespace Nexta.Application.Abstractions.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Basket?> GetByUserIdAsync(ISpecification<Basket> spec, CancellationToken ct = default);
        Task<Basket> AddAsync(Basket basket, CancellationToken ct = default);
        Basket Update(Basket basket);
    }
}