using Nexta.Domain.Models.Product;
using Nexta.Domain.Specification.Abstractions;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id, CancellationToken ct = default);
        Task<Product> GetAllAsync(ISpecification<Product> spec, CancellationToken ct = default);
        Task<Product> AddAsync(Product product, CancellationToken ct = default);
    }
}