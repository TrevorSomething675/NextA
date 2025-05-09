using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IDbContextFactory<MainContext> _contextFactory;

		public UserRepository(IDbContextFactory<MainContext> contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
				return user;
			}
		}

		public async Task<UserEntity?> GetAsync(Guid id, CancellationToken ct = default)
		{
			await using(var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
				return user;
			}
		}

		public async Task<List<UserEntity>> GetAllAsync(CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var users = await context.Users.AsNoTracking().ToListAsync(ct);
				return users;
			}
		}

		public async Task<UserEntity> AddAsync(UserEntity userToAdd, CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var createdUser = (await context.Users.AddAsync(userToAdd, ct)).Entity;
				await context.SaveChangesAsync();
				
				return createdUser;
			}
		}

		public Task<UserEntity> DeleteAsync(Guid id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public Task<UserEntity> UpdateAsync(UserEntity userToUpdate, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}
	}
}
