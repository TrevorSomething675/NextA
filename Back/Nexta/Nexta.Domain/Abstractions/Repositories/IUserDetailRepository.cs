using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserDetailRepository
    {
        Task<UserDetailEntity?> Get(Guid userId, Guid detailId, CancellationToken ct = default);
        Task<UserDetailEntity> Add(UserDetailEntity userDetailToAdd, CancellationToken ct = default);
        Task<UserDetailEntity> Delete(UserDetailEntity userDetailToAdd, CancellationToken ct = default);
	}
}