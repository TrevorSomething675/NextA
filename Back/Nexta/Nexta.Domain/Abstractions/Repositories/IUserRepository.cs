using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
		Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
		Task<User?> GetAsync(Guid id, CancellationToken ct = default);
		Task<List<User>> GetAllAsync(CancellationToken ct = default);

		Task<User> AddAsync(User user, CancellationToken ct = default);
		Task<User> UpdateAsync(User user, CancellationToken ct = default);
	}
}