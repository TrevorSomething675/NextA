using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class BasketProductRepository : IBasketProductRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public BasketProductRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<List<BasketProduct>> GetAllAsync(Guid userId, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductEntities = await context.BasketProducts
                    .AsNoTracking()
                    .Where(bp => bp.UserId == userId)
                    .Include(bp => bp.Product)
                    .ToListAsync(ct);

                var basketProducts = _mapper.Map<List<BasketProduct>>(basketProductEntities);

                return basketProducts;
            }
        }

        public async Task<BasketProduct> AddAsync(BasketProduct basketProductToAdd, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductEntity = _mapper.Map<BasketProductEntity>(basketProductToAdd);

                await context.BasketProducts.AddAsync(basketProductEntity, ct);
                await context.SaveChangesAsync(ct);

                var createdBasketProduct = await context.BasketProducts
                    .AsNoTracking()
                    .Include(b => b.Product)
                    .FirstAsync(b => b.ProductId == basketProductToAdd.ProductId &&
                        b.UserId == basketProductToAdd.UserId, ct);

                var basketProduct = _mapper.Map<BasketProduct>(createdBasketProduct);

                return basketProduct;
            }
        }

        public async Task<BasketProduct> DeleteAsync(BasketProduct basketProductToDelete, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductEntity = _mapper.Map<BasketProductEntity>(basketProductToDelete);
                var deletedBasketProduct = context.BasketProducts.Remove(basketProductEntity);
                await context.SaveChangesAsync(ct);

                var basketProduct = _mapper.Map<BasketProduct>(deletedBasketProduct.Entity);

                return basketProduct;
            }
        }

        public async Task<List<BasketProduct>> DeleteRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductsToDelete = await context.BasketProducts
                    .Where(ud => ud.UserId == userId && productIds.Contains(ud.ProductId))
                    .ToListAsync(ct);

                context.RemoveRange(basketProductsToDelete);
                await context.SaveChangesAsync(ct);

                var basketProducts = _mapper.Map<List<BasketProduct>>(basketProductsToDelete);

                return basketProducts;
            }
        }

        public async Task<BasketProduct?> GetAsync(Guid userId, Guid productId, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductEntity = await context.BasketProducts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.ProductId == productId && u.UserId == userId, ct);

                var basketProduct = _mapper.Map<BasketProduct>(basketProductEntity);

                return basketProduct;
            }
        }

        public async Task<List<BasketProduct>?> GetRangeAsync(Guid userId, List<Guid> productIds, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketDetailsEntities = await context.BasketProducts
                    .AsNoTracking()
                    .Where(ud => ud.UserId == userId && productIds.Contains(ud.ProductId))
                    .ToListAsync(ct);

                var basketDetails = _mapper.Map<List<BasketProduct>>(basketDetailsEntities);

                return basketDetails;
            }
        }



        public async Task<BasketProduct> UpdateAsync(BasketProduct basketProductToUpdate, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var basketProductEntity = await context.BasketProducts
                    .FirstOrDefaultAsync(ud => ud.ProductId == basketProductToUpdate.ProductId && ud.UserId == basketProductToUpdate.UserId, ct);

                if (basketProductEntity == null)
                    throw new DirectoryNotFoundException(string.Format("Связи между пользователем {0} и позицией {1} не существует", basketProductToUpdate?.UserId, basketProductToUpdate?.ProductId));

                if (basketProductToUpdate.Count != null)
                    basketProductEntity.Count = basketProductToUpdate.Count.Value;

                if (basketProductToUpdate.Status != null)
                    basketProductEntity.Status = basketProductToUpdate.Status.Value;

                if (basketProductToUpdate.DeliveryDate != null)
                    basketProductEntity.DeliveryDate = basketProductToUpdate.DeliveryDate.Value;

                await context.SaveChangesAsync(ct);
                var basketProduct = _mapper.Map<BasketProduct>(basketProductEntity);

                return basketProduct;
            }
        }
    }
}
