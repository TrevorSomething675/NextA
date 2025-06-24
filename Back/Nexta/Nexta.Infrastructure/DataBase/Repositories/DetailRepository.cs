using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;
using Nexta.Domain.Extensions;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class DetailRepository : IDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public DetailRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<List<DetailEntity>> GetRangeAsync(List<Guid> detailIds, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var details = await context.Details
					.AsNoTracking()
					.Where(d => detailIds.Contains(d.Id))
					.ToListAsync(ct);

				return details;
			}
		}

		public async Task<PagedData<DetailEntity>> SearchDetail(SearchDetailFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var searchTerm = filter.SearchTerm.ToLower();

				var details = await context.Details
					.AsNoTracking()
					.WithSearchTerm(searchTerm)
					.Skip((filter.PageNumber - 1) * 8)
					.Take(8)
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
					.Include(d => d.Image)
					.Include(d => d.UserDetails)
					.FirstOrDefaultAsync(d => d.Id == id, ct);

				return detail;
			}
		}

		public async Task<PagedData<DetailEntity>> GetAllAsync(GetDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var searchTerm = filter.SearchTerm.ToLower();

				var query = context.Details
					.AsNoTracking();

				var details = await query
					.WithSearchTerm(searchTerm)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var countDetails = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)countDetails / filter.PageSize);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<PagedData<DetailEntity>> GetWarehouseDetailsAsync(BaseFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Details
					.AsNoTracking()
					.Where(d => d.Count > 0);
		
				var details = await query
					.Skip((filter.PageNumber - 1) * 8)
					.Take(8)
					.ToListAsync(ct);
		
				var countDetais = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)countDetais / 8);
		
				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<List<DetailEntity>> GetBasketDetailsAsync(GetBasketDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var details = await context.Details
					.AsNoTracking()
					.Where(d => d.UserDetails.Any(ud => ud.UserId == filter.UserId))
					.Include(d => d.UserDetails.Where(ud => ud.UserId == filter.UserId))
					.ToListAsync(ct);

				return details;
			}
		}
	}
}
