using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Images;
using AutoMapper;
using Nexta.Domain.Abstractions.Repositories;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class NewsImageRepository : INewsImageRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public NewsImageRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(NewsImage imageToAdd, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var imageEntityToAdd = _mapper.Map<NewsImage>(imageToAdd);
                var result = await context.AddAsync(imageEntityToAdd, ct);
                await context.SaveChangesAsync(ct);

                return result.Entity.Id;
            }
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var imageEntityToDelete = await context.NewsImages.FindAsync(id, ct);
                var result = context.NewsImages.Remove(imageEntityToDelete);
                await context.SaveChangesAsync(ct);

                return result.Entity.Id;
            }
        }
    }
}
