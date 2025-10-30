using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Baskets;
using Nexta.Infrastructure.Extensions;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly MainContext _context;

        public BasketRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Basket> AddAsync(Basket basket, CancellationToken ct = default)
        {
            var result = await _context.Basket.AddAsync(basket, ct);
            return result.Entity;
        }

        public async Task<Basket?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var result = await _context.Basket.FirstOrDefaultAsync(b => b.Id == id, ct);
            return result;
        }

        public async Task<Basket?> GetByUserIdAsync(ISpecification<Basket> spec, CancellationToken ct = default)
        {
            var result = await _context.Basket
                .Includes(spec.Includes)
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