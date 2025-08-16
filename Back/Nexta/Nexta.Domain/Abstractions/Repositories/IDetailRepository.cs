using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IDetailRepository
    {
        Task<PagedData<Detail>> SearchDetail(SearchDetailFilter filter, CancellationToken ct = default);

        Task<Detail?> GetAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<Detail>> GetAllAsync(GetDetailsFilter filter, CancellationToken ct = default);
        Task<List<Detail>> GetBasketDetailsAsync(GetBasketDetailsFilter filter, CancellationToken ct = default);
        Task<List<Detail>> GetRangeAsync(List<Guid> detailIds, CancellationToken ct = default);

        Task<Detail> UpdateAsync(Detail detail, CancellationToken ct = default);
        Task<Guid> CreateAsync(Detail detail, CancellationToken ct = default);
    }
}