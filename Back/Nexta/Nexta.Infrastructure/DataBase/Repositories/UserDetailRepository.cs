using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class UserDetailRepository : IUserDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public UserDetailRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<UserDetailEntity?> GetAsync(Guid userId, Guid detailId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetail = await context.UserDetails
					.FirstOrDefaultAsync(u => u.DetailId == detailId && u.UserId == userId, ct);

				return userDetail;
			}
		}

		public async Task<UserDetailEntity> AddAsync(UserDetailEntity userDetailToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var createdUserDetail = await context.UserDetails.AddAsync(userDetailToAdd, ct);

				await context.SaveChangesAsync(ct);

				return createdUserDetail.Entity;
			}
		}

		public async Task<UserDetailEntity> DeleteAsync(UserDetailEntity userDetailToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailsToDelete = context.UserDetails.Remove(userDetailToAdd);
				await context.SaveChangesAsync(ct);

				return userDetailsToDelete.Entity;
			}
		}
	}
}