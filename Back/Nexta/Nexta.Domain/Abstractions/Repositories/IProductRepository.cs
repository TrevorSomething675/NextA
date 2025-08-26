using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters.Products;
using Nexta.Domain.Filters;
using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<Product>> GetAllAsync(GetProductsFilter filter, CancellationToken ct = default);
        Task<List<Product>> GetBasketProductsAsync(GetBasketProductsFilter filter, CancellationToken ct = default);
        Task<List<Product>> GetRangeAsync(List<Guid> detailIds, CancellationToken ct = default);

        Task<Product> UpdateAsync(Product detail, CancellationToken ct = default);
        Task<Guid> CreateAsync(Product detail, CancellationToken ct = default);
    }
}