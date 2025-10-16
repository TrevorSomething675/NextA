using Nexta.Domain.Filters.Users;
using Nexta.Domain.Base;
using Nexta.Domain.Models.User;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
		Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
		Task<User?> GetAsync(Guid id, CancellationToken ct = default);
		Task<PagedData<User>> GetAllAsync(GetAdminUsersFilter filter, CancellationToken ct = default);

		Task<User> AddAsync(User user, CancellationToken ct = default);
		Task<User> UpdateAsync(User user, CancellationToken ct = default);
		Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default);
	}
}