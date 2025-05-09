using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IDetailRepository
    {
        Task<DetailEntity?> GetAsync(Guid id, CancellationToken ct);
        Task<PagedData<DetailEntity>> GetAllAsync(BaseFilter filter, CancellationToken ct);
        Task<PagedData<DetailEntity>> GetWarehouseDetailsAsync(BaseFilter filter, CancellationToken ct = default);
        Task<List<DetailEntity>> GetBasketDetailsAsync(BasketDetailsFilter filter, CancellationToken ct = default);

		Task<DetailEntity> AddAsync(DetailEntity detailToAdd, CancellationToken ct);
        Task<DetailEntity> UpdateAsync(DetailEntity detailToUpdate, CancellationToken ct);
        Task<DetailEntity> DeleteAsync(Guid id, CancellationToken ct);
    }
}