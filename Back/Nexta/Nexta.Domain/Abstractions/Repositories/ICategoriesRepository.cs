using Nexta.Domain.Models.Product;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface ICategoriesRepository 
    {
        Task<List<Category>> GetAsync(CancellationToken ct = default);
        Task<Category?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Guid> AddAsync(Category category, CancellationToken ct = default);
        Guid Delete(Category category, CancellationToken ct = default);
    }
}