using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;
using Microsoft.EntityFrameworkCore.Internal;
using Nexta.Domain.Models;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class DetailRepository : IDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public DetailRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<DetailEntity?> Get(Guid id, CancellationToken ct)
		{
			await using(var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var detail = await context.Details.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
				return detail;
			}
		}

		public async Task<PagedData<DetailEntity>> GetAll(BaseFilter filter, CancellationToken ct)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var details = await context.Details
					.AsNoTracking()
					.Skip(filter.PageNumber - 1)
					.Take(new Range((filter.PageNumber - 1) * 8, filter.PageNumber * 8))
					.ToListAsync();

				var countDetails = await context.Details
					.AsNoTracking()
					.Select(d => d.Id)
					.ToListAsync(ct);

				var pageCount = (int)Math.Ceiling((double)countDetails.Count / 8);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
			}
		}

		public async Task<List<DetailEntity>> GetBasketDetails(BasketDetailsFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetails = await context.UserDetails
					.AsNoTracking()
					.Include(ud => ud.Detail)
					.Where(ud => ud.UserId == filter.UserId)
					.Select(d => d.Detail)
					.ToListAsync(ct);

				var pageCount = (int)Math.Ceiling((double)userDetails.Count / 8);

				return userDetails;
			}
		}

		public Task<DetailEntity> Add(DetailEntity detailToAdd, CancellationToken ct)
		{
			throw new NotImplementedException();
		}

		public Task<DetailEntity> Delete(Guid id, CancellationToken ct)
		{
			throw new NotImplementedException();
		}

		public Task<DetailEntity> Update(DetailEntity detailToUpdate, CancellationToken ct)
		{
			throw new NotImplementedException();
		}
	}
}
