using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserRepository
    {
		Task<UserEntity?> GetByEmailAsync(string email);
		Task<UserEntity?> GetAsync(Guid id);
		Task<List<UserEntity>> GetAllAsync();

		Task<UserEntity> AddAsync(UserEntity userToAdd);
		Task<UserEntity> UpdateAsync(UserEntity userToUpdate);
		Task<UserEntity> DeleteAsync(Guid id);
	}
}