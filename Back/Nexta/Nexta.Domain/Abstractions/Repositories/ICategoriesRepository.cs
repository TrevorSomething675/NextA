using Nexta.Domain.Models.Product;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface ICategoriesRepository 
    {
        Task<Category> GetByName(string name, CancellationToken ct = default);
        Task<List<Category>> GetAsync(CancellationToken ct = default);
        Task<Guid> AddAsync(Category category, CancellationToken ct = default);
        Guid Delete(Category name, CancellationToken ct = default);
    }
}