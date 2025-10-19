using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Basket;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly MainContext _context;

        public BasketRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Basket?> GetAsync(Guid id, CancellationToken ct = default)
        {
            var result = await _context.Basket.FirstOrDefaultAsync(b => b.Id == id, ct);
            return result;
        }

        public async Task<Basket?> GetByUserIdAsync(ISpecification<Basket> spec, CancellationToken ct = default)
        {
            var result = await _context.Basket
                .Include(b => spec.Includes)
                .FirstOrDefaultAsync(spec.Creteria, ct);
            return result;
        }

        public Basket Update(Basket basket)
        {
            var result = _context.Basket.Update(basket);
            return result.Entity;
        }
    }
}