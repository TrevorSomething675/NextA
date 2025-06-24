using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
		Task<UserEntity?> GetByEmailAsync(string email, CancellationToken ct);
		Task<UserEntity?> GetAsync(Guid id, CancellationToken ct);
		Task<List<UserEntity>> GetAllAsync(CancellationToken ct);

		Task<UserEntity> AddAsync(UserEntity userToAdd, CancellationToken ct);
	}
}