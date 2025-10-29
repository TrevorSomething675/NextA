using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Basket;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Basket?> GetByUserIdAsync(ISpecification<Basket> spec, CancellationToken ct = default);
        Basket Update(Basket basket);
    }
}