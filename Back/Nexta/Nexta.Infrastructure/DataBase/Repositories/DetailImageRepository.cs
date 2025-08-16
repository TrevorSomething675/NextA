using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Images;
using AutoMapper;
using Nexta.Domain.Exceptions;
using Nexta.Infrastructure.DataBase.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class DetailImageRepository : IDetailImageRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public DetailImageRepository(IDbContextFactory<MainContext> contextFactory, IMapper mapper)
        {
            _dbContextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(DetailImage detailImage, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var detailEntity = _mapper.Map<DetailImageEntity>(detailImage);

                var createdDetailImage = await context.DetailImages.AddAsync(detailEntity, ct);

                await context.SaveChangesAsync(ct);

                return createdDetailImage.Entity.Id;
            }
        }

        public async Task<Guid> RemoveAsync(Guid id, CancellationToken ct = default)
        {
            await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var detailImageToRemove = await context.DetailImages
                    .Include(di => di.Detail)
                    .FirstOrDefaultAsync(di => di.Id == id, ct);

                if (detailImageToRemove == null)
                    throw new NotFoundException("Не найдена картинка для детали");

                context.DetailImages.Remove(detailImageToRemove);
                await context.SaveChangesAsync(ct);

                return detailImageToRemove.Id;
            }
        }

        public async Task<DetailImage> UpdateAsync(DetailImage detailImage, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var detailImageEntity = await context.DetailImages.FindAsync(detailImage.Id);

                detailImageEntity.Base64String = detailImage.Base64String;
                detailImageEntity.Name = detailImage.Name;

                await context.SaveChangesAsync(ct);

                return _mapper.Map<DetailImage>(detailImage);
            }
        }
    }
}
