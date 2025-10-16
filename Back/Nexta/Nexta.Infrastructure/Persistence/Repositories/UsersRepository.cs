using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Base;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Filters.Users;
using Nexta.Domain.Models.User;
using Nexta.Infrastructure.Extensions;
using Nexta.Infrastructure.Persistence;
using Nexta.Infrastructure.Persistence.Entities;

namespace Nexta.Infrastructure.Persistence.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		private readonly IDbContextFactory<MainContext> _contextFactory;
		private readonly IMapper _mapper;

		public UsersRepository(IDbContextFactory<MainContext> contextFactory, IMapper mapper)
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

		public async Task<PagedData<User>> GetAllAsync(GetAdminUsersFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var searchTerm = filter.SearchTerm.ToLower() ?? "";

				var query = context.Users
					.AsNoTracking()
					.WithSearchTerm(searchTerm);

				var userEntities = await query
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

                var countProducts = await query.CountAsync(ct);
                var pageCount = (int)Math.Ceiling((double)countProducts / filter.PageSize);

				var pagedUserEntities = new PagedData<UserEntity>(userEntities, userEntities.Count, pageCount);

				var result = _mapper.Map<PagedData<User>>(pagedUserEntities);

				return result;
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

        public async Task<User> UpdateAsync(User user, CancellationToken ct = default)
        {
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userEntity = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id, ct);

				if (userEntity == null)
					throw new NotFoundException("Пользователь не найден");
				   
				if (user.FirstName != null) userEntity.FirstName = user.FirstName;
				if (user.MiddleName != null) userEntity.MiddleName = user.MiddleName;
				if (user.LastName != null) userEntity.LastName = user.LastName;

				if (user.Email != null) userEntity.Email = user.Email;
				if (user.Phone != null) userEntity.Phone = user.Phone;

				if (user.PasswordHash != null) userEntity.PasswordHash = user.PasswordHash;

				var result = context.Users.Update(userEntity);
				await context.SaveChangesAsync(ct);

				var updatedUser = _mapper.Map<User>(result.Entity);

				return updatedUser;
            }
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default)
        {
			await using (var context = await _contextFactory.CreateDbContextAsync(ct))
			{
				var userToDelete = await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

				var result = context.Users.Remove(userToDelete);
				await context.SaveChangesAsync(ct);

				return result.Entity.Id;
			}
        }
    }
}
