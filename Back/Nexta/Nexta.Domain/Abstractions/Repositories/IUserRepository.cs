using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
		Task<User?> GetByEmailAsync(string email, CancellationToken ct);
		Task<User?> GetAsync(Guid id, CancellationToken ct);
		Task<List<User>> GetAllAsync(CancellationToken ct);

		Task<User> AddAsync(User user, CancellationToken ct);
	}
}