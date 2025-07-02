using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Filters;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Infrastructure.Extensions;
using Nexta.Infrastructure.DataBase.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class DetailRepository : IDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;
		public DetailRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public async Task<List<Detail>> GetRangeAsync(List<Guid> detailIds, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var detailEntities = await context.Details
					.AsNoTracking()
					.Where(d => detailIds.Contains(d.Id))
					.ToListAsync(ct);

				var details = _mapper.Map<List<Detail>>(detailEntities);

				return details;
			}
		}

		public async Task<PagedData<Detail>> SearchDetail(SearchDetailFilter filter, CancellationToken ct = default)
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

				var pagedDetailEntities = new PagedData<DetailEntity>(details, details.Count, pageCount);

				var pagedDetails = _mapper.Map<PagedData<Detail>>(pagedDetailEntities);

				return pagedDetails;
			}
		}

		public async Task<Detail?> GetAsync(Guid id, CancellationToken ct = default)
		{
			await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var detailEntity = await context.Details.AsNoTracking()
					.Include(d => d.Image)
					.Include(d => d.UserDetails)
					.FirstOrDefaultAsync(d => d.Id == id, ct);

				var detail = _mapper.Map<Detail>(detailEntity);

				return detail;
			}
		}

		public async Task<PagedData<Detail>> GetAllAsync(GetDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var searchTerm = filter.SearchTerm.ToLower();

				var query = context.Details
					.Where(d => d.IsVisible || filter.WithHidden)
					.AsNoTracking();

				var detailEntities = await query
					.WithSearchTerm(searchTerm)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var countDetails = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)countDetails / filter.PageSize);

				var pagedDetailEntities = new PagedData<DetailEntity>(detailEntities, detailEntities.Count, pageCount);

				var pagedDetails = _mapper.Map<PagedData<DetailEntity>, PagedData<Detail>>(pagedDetailEntities);

				return pagedDetails;
			}
		}

		public async Task<PagedData<Detail>> GetWarehouseDetailsAsync(BaseFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Details
					.AsNoTracking()
					.Where(d => d.Count > 0);
		
				var detailEntities = await query
					.Skip((filter.PageNumber - 1) * 8)
					.Take(8)
					.ToListAsync(ct);
		
				var countDetails = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)countDetails / 8);
		
				var pagedEntities = new PagedData<DetailEntity>(detailEntities, detailEntities.Count, pageCount);
				var pagedDetails = _mapper.Map<PagedData<Detail>>(pagedEntities);

				return pagedDetails;
			}
		}

		public async Task<List<Detail>> GetBasketDetailsAsync(GetBasketDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var detailEntities = await context.Details
					.AsNoTracking()
					.Where(d => d.UserDetails.Any(ud => ud.UserId == filter.UserId))
					.Include(d => d.UserDetails.Where(ud => ud.UserId == filter.UserId))
					.ToListAsync(ct);

				var details = _mapper.Map<List<Detail>>(detailEntities);

				return details;
			}
		}
	}
}