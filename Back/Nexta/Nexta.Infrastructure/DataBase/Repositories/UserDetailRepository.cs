using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class UserDetailRepository : IUserDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;

		public UserDetailRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public async Task<UserDetail?> GetAsync(Guid userId, Guid detailId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailEntity = await context.UserDetails
					.FirstOrDefaultAsync(u => u.DetailId == detailId && u.UserId == userId, ct);

				var userDetail = _mapper.Map<UserDetail>(userDetailEntity);

				return userDetail;
			}
		}

		public async Task<UserDetail> AddAsync(UserDetail userDetailToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailEntity = _mapper.Map<UserDetailEntity>(userDetailToAdd);
				var createdUserDetail = (await context.UserDetails.AddAsync(userDetailEntity, ct)).Entity;

				await context.SaveChangesAsync(ct);

				var userDetail = _mapper.Map<UserDetail>(createdUserDetail);

				return userDetail;
			}
		}

		public async Task<UserDetail> DeleteAsync(UserDetail userDetailToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailEntity = _mapper.Map<UserDetailEntity>(userDetailToAdd);
				var userDetailToDelete = context.UserDetails.Remove(userDetailEntity);
				await context.SaveChangesAsync(ct);

				var userDetail = _mapper.Map<UserDetail>(userDetailToDelete);

				return userDetail;
			}
		}

		public async Task<List<UserDetail>> DeleteRangeAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailsToDelete = await context.UserDetails
					.Where(ud => ud.UserId == userId && detailIds.Contains(ud.DetailId))
					.ToListAsync(ct);

				context.RemoveRange(userDetailsToDelete);
				await context.SaveChangesAsync(ct);

				var userDetails = _mapper.Map<List<UserDetail>>(userDetailsToDelete);

				return userDetails;
			}
		}

		public async Task<List<UserDetail>?> GetRangeAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var userDetailEntities = await context.UserDetails
					.AsNoTracking()
					.Where(ud => ud.UserId == userId && detailIds.Contains(ud.DetailId))
					.ToListAsync(ct);

				var userDetails = _mapper.Map<List<UserDetail>>(userDetailEntities);

				return userDetails;
			}
		}
	}
}