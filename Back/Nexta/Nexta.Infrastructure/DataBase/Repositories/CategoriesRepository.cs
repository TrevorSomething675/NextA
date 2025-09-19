using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Exceptions;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public CategoriesRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(ProductCategory category, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var categoryEntity = _mapper.Map<ProductCategoryEntity>(category);

                var result = await context.AddAsync(categoryEntity, ct);
                await context.SaveChangesAsync(ct);

                return result.Entity.Id;
            }
        }

        public async Task<Guid> DeleteAsync(string name, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var dbCategory = await context.ProductCategories
                    .FirstOrDefaultAsync(c => c.Name == name);

                if (dbCategory == null)
                    throw new NotFoundException("Категория не найдена");

                var result = context.ProductCategories.Remove(dbCategory);
                await context.SaveChangesAsync(ct);

                return result.Entity.Id;
            }
        }

        public async Task<List<ProductCategory>> GetAsync(CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var categoryEntities = await context.ProductCategories
                    .AsNoTracking()
                    .ToListAsync(ct);

                var categories = _mapper.Map<List<ProductCategory>>(categoryEntities);

                return categories;
            }
        }
    }
}
