using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Product;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly MainContext _context;

        public CategoriesRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByName(string name, CancellationToken ct = default)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name, ct);
            return category;
        }

        public async Task<Guid> AddAsync(Category category, CancellationToken ct = default)
        {
            var result = await _context.Categories.AddAsync(category, ct);
            return result.Entity.Id;
        }

        public Guid Delete(Category category, CancellationToken ct = default)
        {
            _context.Categories.Remove(category);
            return category.Id;
        }

        public async Task<List<Category>> GetAsync(CancellationToken ct = default)
        {
            var categories = await _context.Categories.ToListAsync(ct);
            return categories;
        }
    }
}
