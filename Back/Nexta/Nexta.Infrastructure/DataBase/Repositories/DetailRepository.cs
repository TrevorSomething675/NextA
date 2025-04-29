using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class DetailRepository : IDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _contextFactory;

		public DetailRepository(IDbContextFactory<MainContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<DetailEntity?> Get(Guid id, CancellationToken ct)
		{
			await using(var context = await _contextFactory.CreateDbContextAsync())
			{
				var detail = await context.Details.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
				return detail;
			}
		}

		public async Task<PagedData<DetailEntity>> GetAll(DetailsFilter filter, CancellationToken ct)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync())
			{
				var details = await context.Details
					.AsNoTracking()
					.Skip(filter.PageNumber - 1)
					.Take(new Range((filter.PageNumber - 1) * 8, filter.PageNumber * 8))
					.ToListAsync();


				var pageCount = (int)Math.Ceiling((double)details.Count / 8);

				return new PagedData<DetailEntity>(details, details.Count, pageCount);
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
