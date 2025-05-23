using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class DetailRepository : IDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public DetailRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<PagedData<DetailEntity>> SearchDetail(SearchDetailFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var searchTerm = filter.SearchTerm.ToLower();

				var details = await context.Details.Where(d =>
					EF.Functions.Like(d.Article.ToLower(), $"{searchTerm}") ||
					EF.Functions.Like(d.Article.ToLower(), $"{searchTerm}%") ||
					EF.Functions.Like(d.Article.ToLower(), $"%{searchTerm}") ||
					EF.Functions.Like(d.Name.ToLower(), $"{searchTerm}") ||
					EF.Functions.Like(d.Name.ToLower(), $"{searchTerm}%") ||
					EF.Functions.Like(d.Name.ToLower(), $"%{searchTerm}"))
					.Skip((filter.PageNumber - 1) * 8)
					.Take(filter.PageNumber * 8)
					.ToListAsync(ct);

				var pageCount = (int)Math.Ceiling((double)details.Count / 8);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<DetailEntity?> GetAsync(Guid id, CancellationToken ct = default)
		{
			await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var detail = await context.Details.AsNoTracking()
					.Include(d => d.UserDetail)
					.FirstOrDefaultAsync(d => d.Id == id, ct);

				return detail;
			}
		}

		public async Task<PagedData<DetailEntity>> GetAllAsync(BaseFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var details = await context.Details
					.AsNoTracking()
					.Skip((filter.PageNumber - 1) * 8)
					.Take(filter.PageNumber * 8)
					.ToListAsync(ct);

				var countDetails = await context.Details
					.AsNoTracking()
					.Select(d => d.Id)
					.ToListAsync(ct);

				var pageCount = (int)Math.Ceiling((double)countDetails.Count / 8);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<PagedData<DetailEntity>> GetWarehouseDetailsAsync(BaseFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var details = await context.Details
					.AsNoTracking()
					.Where(d => d.Count > 0)
					.Skip((filter.PageNumber - 1) * 8)
					.Take(filter.PageNumber * 8)
					.ToListAsync(ct);

				var countDetais = await context.Details
					.AsNoTracking()
					.Where(d => d.Count > 0)
					.Select(d => d.Id)
					.ToListAsync(ct);

				var pageCount = (int)Math.Ceiling((double)countDetais.Count / 8);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<List<DetailEntity>> GetBasketDetailsAsync(BasketDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var details = await context.Details
					.AsNoTracking()
					.Where(d => d.UserDetail.Any(ud => ud.UserId == filter.UserId))
					.Include(d => d.UserDetail)
					.ToListAsync(ct);

				foreach (var detail in details)
				{
					detail.UserDetail = detail.UserDetail?.Where(ud => ud.UserId == filter.UserId).ToList();
				}

				return details;
			}
		}

		public Task<DetailEntity> AddAsync(DetailEntity detailToAdd, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<DetailEntity> DeleteAsync(Guid id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<DetailEntity> UpdateAsync(DetailEntity detailToUpdate, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}
