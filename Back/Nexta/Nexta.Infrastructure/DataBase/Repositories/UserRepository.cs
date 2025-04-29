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

		public async Task<UserEntity?> GetByEmailAsync(string email)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync())
			{
				var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
				return user;
			}
		}

		public async Task<UserEntity?> GetAsync(Guid id)
		{
			await using(var context = await _contextFactory.CreateDbContextAsync())
			{
				var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
				return user;
			}
		}

		public async Task<List<UserEntity>> GetAllAsync()
		{
			await using (var context = await _contextFactory.CreateDbContextAsync())
			{
				var users = await context.Users.AsNoTracking().ToListAsync();
				return users;
			}
		}

		public async Task<UserEntity> AddAsync(UserEntity userToAdd)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync())
			{
				var createdUser = context.Users.Add(userToAdd).Entity;
				await context.SaveChangesAsync();
				
				return createdUser;
			}
		}

		public Task<UserEntity> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<UserEntity> UpdateAsync(UserEntity userToUpdate)
		{
			throw new NotImplementedException();
		}
	}
}
