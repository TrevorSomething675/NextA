using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserDetailRepository
    {
        Task<UserDetailEntity?> GetAsync(Guid userId, Guid detailId, CancellationToken ct = default);
        Task<UserDetailEntity> AddAsync(UserDetailEntity userDetailToAdd, CancellationToken ct = default);
        Task<UserDetailEntity> DeleteAsync(UserDetailEntity userDetailToAdd, CancellationToken ct = default);
	}
}