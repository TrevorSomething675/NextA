using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Users;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
		Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
		Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
		Task<PagedData<User>> GetAsync(ISpecification<User> spec, CancellationToken ct = default);

		Task<User> AddAsync(User user, CancellationToken ct = default);
		User Update(User user);
		User Delete(User user);
	}
}