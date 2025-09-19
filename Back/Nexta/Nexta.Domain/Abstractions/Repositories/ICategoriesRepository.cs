using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface ICategoriesRepository 
    {
        Task<List<ProductCategory>> GetAsync(CancellationToken ct = default);
        Task<Guid> AddAsync(ProductCategory category, CancellationToken ct = default);
        Task<Guid> DeleteAsync(string name, CancellationToken ct = default);
    }
}