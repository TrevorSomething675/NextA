using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Product;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<Product>> GetAllAsync(ISpecification<Product> spec, CancellationToken ct = default);
        Task<Product> AddAsync(Product product, CancellationToken ct = default);
    }
}