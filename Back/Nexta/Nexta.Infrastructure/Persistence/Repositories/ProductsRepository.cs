using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Product;
using Nexta.Domain.Base;

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

        public async Task<PagedData<Product>> GetAllAsync(ISpecification<Product> spec, CancellationToken ct = default)
        {
            var query = _context.Products
                .WithSearchTerm(spec.SearchTerm)
                .WithIncludes(spec.Includes)
                .Where(spec.Creteria);

            var products = await query
                .Skip((spec.PageNumber - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync(ct);

            var productsCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)productsCount / spec.PageSize);

            var pagedProducts = new PagedData<Product>(products, products.Count, pageCount);

            return pagedProducts;
        }

        public async Task<Product> GetAsync(Guid id, CancellationToken ct = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
            return product;
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> productIds, CancellationToken ct = default)
        {
            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync(ct);

            return products;
        }
    }
}