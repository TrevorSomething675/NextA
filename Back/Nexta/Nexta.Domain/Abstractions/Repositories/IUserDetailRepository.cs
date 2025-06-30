using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IUserDetailRepository
    {
        Task<UserDetail?> GetAsync(Guid userId, Guid detailId, CancellationToken ct = default);
        Task<List<UserDetail>?> GetRangeAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default);
        Task<UserDetail> AddAsync(UserDetail userDetailToAdd, CancellationToken ct = default);
        Task<UserDetail> DeleteAsync(UserDetail userDetailToAdd, CancellationToken ct = default);
        Task<List<UserDetail>> DeleteRangeAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default);
	}
}