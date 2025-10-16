using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Nexta.Infrastructure.Persistence.Entities;
using Nexta.Domain.Models.Basket;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class BasketProductRepository : IBasketProductRepository
    {
        private readonly MainContext _dbContext;
        private readonly IMapper _mapper;

        public BasketProductRepository(MainContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<BasketProduct>> GetAllAsync(Guid userId, CancellationToken ct = default)
        {
            var basketProductEntities = await _dbContext.BasketProducts
                .AsNoTracking()
                .Where(bp => bp.UserId == userId)
                .Include(bp => bp.Product)
                .ToListAsync(ct);

            var basketProducts = _mapper.Map<List<BasketProduct>>(basketProductEntities);

            return basketProducts;
        }

        public async Task<BasketProduct> AddAsync(BasketProduct basketProductToAdd, CancellationToken ct = default)
        {
            var basketProductEntity = _mapper.Map<BasketProductEntity>(basketProductToAdd);

            var result = await _dbContext.BasketProducts.AddAsync(basketProductEntity, ct);
            
            var basketProduct = _mapper.Map<BasketProduct>(result.Entity);

            return basketProduct;
        }

        public async Task<BasketProduct> DeleteAsync(BasketProduct basketProductToDelete, CancellationToken ct = default)
        {
            var basketProductEntity = _mapper.Map<BasketProductEntity>(basketProductToDelete);
            var deletedBasketProduct = _dbContext.BasketProducts.Remove(basketProductEntity);

            var basketProduct = _mapper.Map<BasketProduct>(deletedBasketProduct.Entity);

            return basketProduct;
        }

        public async Task<List<BasketProduct>> DeleteRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default)
        {
            var basketProductsToDelete = await _dbContext.BasketProducts
                .Where(ud => ud.UserId == userId && productIds.Contains(ud.ProductId))
                .ToListAsync(ct);

            _dbContext.RemoveRange(basketProductsToDelete);

            var basketProducts = _mapper.Map<List<BasketProduct>>(basketProductsToDelete);

            return basketProducts;
        }

        public async Task<BasketProduct?> GetAsync(Guid userId, Guid productId, CancellationToken ct = default)
        {
            var basketProductEntity = await _dbContext.BasketProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ProductId == productId && u.UserId == userId, ct);

            var basketProduct = _mapper.Map<BasketProduct>(basketProductEntity);

            return basketProduct;
        }

        public async Task<List<BasketProduct>?> GetRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default)
        {
            var basketProductsEntities = await _dbContext.BasketProducts
                .AsNoTracking()
                .Where(ud => ud.UserId == userId && productIds.Contains(ud.ProductId))
                .ToListAsync(ct);

            var basketProducts = _mapper.Map<List<BasketProduct>>(basketProductsEntities);

            return basketProducts;
        }

        public async Task<BasketProduct> UpdateAsync(BasketProduct basketProductToUpdate, CancellationToken ct = default)
        {
            var basketProductEntity = await _dbContext.BasketProducts
                .FirstOrDefaultAsync(ud => ud.ProductId == basketProductToUpdate.ProductId && ud.UserId == basketProductToUpdate.UserId, ct);

            if (basketProductEntity == null)
                throw new DirectoryNotFoundException(string.Format("Связи между пользователем {0} и позицией {1} не существует", basketProductToUpdate?.UserId, basketProductToUpdate?.ProductId));

            if (basketProductToUpdate.Count != null)
                basketProductEntity.Count = basketProductToUpdate.Count.Value;

            if (basketProductToUpdate.Status != null)
                basketProductEntity.Status = basketProductToUpdate.Status.Value;

            if (basketProductToUpdate.DeliveryDate != null)
                basketProductEntity.DeliveryDate = basketProductToUpdate.DeliveryDate.Value;

            var basketProduct = _mapper.Map<BasketProduct>(basketProductEntity);

            return basketProduct;
        }
    }
}
            /*
             await _dbContext.BasketProducts
                .Where(ud => ud.ProductId == basketProductToUpdate.ProductId && ud.UserId == basketProductToUpdate.UserId)
                .ExecuteUpdateAsync(product => product
                    .SetProperty(p => p.Count, p => basketProductToUpdate.Count.HasValue ? basketProductToUpdate.Count : p.Count)
                    .SetProperty(p => p.Status, p => basketProductToUpdate.Status.HasValue ? basketProductToUpdate.Status : p.Status)
                    .SetProperty(p => p.DeliveryDate, p => basketProductToUpdate.DeliveryDate.HasValue ? basketProductToUpdate.DeliveryDate : p.DeliveryDate), ct);

            var updateProduct = _mapper.Map<BasketProduct>(basketProductToUpdate);
            return updateProduct;
            */
