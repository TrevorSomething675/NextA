using Nexta.Domain.Models.Product;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductsRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken ct = default);
        Task<Product> GetAsync(Guid id, CancellationToken ct = default);
    }
}
