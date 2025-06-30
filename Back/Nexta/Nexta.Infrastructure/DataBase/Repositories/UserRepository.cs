using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IDbContextFactory<MainContext> _contextFactory;
		private readonly IMapper _mapper;

		public UserRepository(IDbContextFactory<MainContext> contextFactory, IMapper mapper)
		{
			_contextFactory = contextFactory;
			_mapper = mapper;
		}

		public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userEntity = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);
				var user = _mapper.Map<User>(userEntity);

				return user;
			}
		}

		public async Task<User?> GetAsync(Guid id, CancellationToken ct = default)
		{
			await using(var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userEntity = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, ct);
				var user = _mapper.Map<User>(userEntity);

				return user;
			}
		}

		public async Task<List<User>> GetAllAsync(CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userEntities = await context.Users.AsNoTracking().ToListAsync(ct);
				var users = _mapper.Map<List<User>>(userEntities);

				return users;
			}
		}

		public async Task<User> AddAsync(User user, CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userToAdd = _mapper.Map<UserEntity>(user);
				var createdUserEntity = (await context.Users.AddAsync(userToAdd, ct)).Entity;
				await context.SaveChangesAsync();

				var createdUser = _mapper.Map<User>(createdUserEntity);
				return createdUser;
			}
		}
	}
}
