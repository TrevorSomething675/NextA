using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IDetailRepository
    {
        Task<PagedData<DetailEntity>> SearchDetail(SearchDetailFilter filter, CancellationToken ct = default);

        Task<DetailEntity?> GetAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<DetailEntity>> GetAllAsync(DetailsFilter filter, CancellationToken ct = default);
        Task<List<DetailEntity>> GetBasketDetailsAsync(BasketDetailsFilter filter, CancellationToken ct = default);
        Task<List<DetailEntity>> GetRangeAsync(List<Guid> detailIds, CancellationToken ct = default);

		Task<DetailEntity> AddAsync(DetailEntity detailToAdd, CancellationToken ct = default);
        Task<DetailEntity> UpdateAsync(DetailEntity detailToUpdate, CancellationToken ct = default);
        Task<DetailEntity> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}