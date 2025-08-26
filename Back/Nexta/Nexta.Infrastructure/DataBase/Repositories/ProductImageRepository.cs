using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models.Images;
using Nexta.Infrastructure.DataBase.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ProductImageRepository(IDbContextFactory<MainContext> contextFactory, IMapper mapper)
        {
            _dbContextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(ProductImage detailImage, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var detailEntity = _mapper.Map<ProductImageEntity>(detailImage);

                var createdDetailImage = await context.ProductImages.AddAsync(detailEntity, ct);

                await context.SaveChangesAsync(ct);

                return createdDetailImage.Entity.Id;
            }
        }

        public async Task<Guid> RemoveAsync(Guid id, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var detailImageToRemove = await context.ProductImages
                    .Include(di => di.Product)
                    .FirstOrDefaultAsync(di => di.Id == id, ct);

                if (detailImageToRemove == null)
                    throw new NotFoundException("Не найдена картинка для детали");

                context.ProductImages.Remove(detailImageToRemove);
                await context.SaveChangesAsync(ct);

                return detailImageToRemove.Id;
            }
        }

        public async Task<ProductImage> UpdateAsync(ProductImage productImage, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productImageEntity = await context.ProductImages.FindAsync(productImage.Id);

                productImageEntity.Base64String = productImage.Base64String;
                productImageEntity.Name = productImage.Name;

                await context.SaveChangesAsync(ct);

                return _mapper.Map<ProductImage>(productImage);
            }
        }
    }
}
