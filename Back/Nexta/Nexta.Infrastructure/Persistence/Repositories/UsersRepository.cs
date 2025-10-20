using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Filters.Users;
using Nexta.Domain.Models.User;
using Nexta.Domain.Base;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public Task<User> AddAsync(User user, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<PagedData<User>> GetAllAsync(GetAdminUsersFilter filter, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User user, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}