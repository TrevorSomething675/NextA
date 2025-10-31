using Nexta.Domain.Models.Products;
using Nexta.Domain.Base;
using Nexta.Application.Abstractions.Specification;

namespace Nexta.Application.Abstractions.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<Product>> GetByIdsAsync(List<Guid> productIds, CancellationToken ct = default);
        Task<PagedData<Product>> GetAsync(ISpecification<Product> spec, CancellationToken ct = default);
        
        Task<Product> AddAsync(Product product, CancellationToken ct = default);
    }
}