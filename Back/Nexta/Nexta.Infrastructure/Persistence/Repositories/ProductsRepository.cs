using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Product;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly MainContext _context;

        public ProductsRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
        {
            var result = await _context.Products.AddAsync(product, ct);
            return result.Entity;
        }

        public async Task<Product> GetAsync(Guid id, CancellationToken ct = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
            return product;
        }
    }
}
