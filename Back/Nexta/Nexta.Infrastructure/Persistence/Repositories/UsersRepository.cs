using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Users;
using Nexta.Domain.Base;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MainContext _context;

        public UsersRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, ct);
            return user;
        }

        public async Task<PagedData<User>> GetAsync(ISpecification<User> spec, CancellationToken ct = default)
        {
            var query = _context.Users
                .Where(spec.Creteria)
                .WithSearchTerm(spec.SearchTerm);

            var users = await query
                .Skip((spec.PageNumber - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync(ct);

            var usersCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)usersCount / spec.PageSize);

            var pagedUsers = new PagedData<User>(users, users.Count, pageCount);

            return pagedUsers;
        }

        public async Task<User> AddAsync(User user, CancellationToken ct = default)
        {
            var createdUser = await _context.Users.AddAsync(user, ct);
            return createdUser.Entity;
        }

        public User Delete(User user)
        {
            var deletedUser = _context.Users.Remove(user);
            return deletedUser.Entity;
        }

        public User Update(User user)
        {
            var updatedUser = _context.Users.Update(user);
            return updatedUser.Entity;
        }
    }
}