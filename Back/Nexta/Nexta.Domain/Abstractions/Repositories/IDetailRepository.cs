using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IDetailRepository
    {
        Task<DetailEntity?> Get(Guid id, CancellationToken ct);
        Task<PagedData<DetailEntity>> GetAll(BaseFilter filter, CancellationToken ct);
        Task<List<DetailEntity>> GetBasketDetails(BasketDetailsFilter filter, CancellationToken ct = default);

		Task<DetailEntity> Add(DetailEntity detailToAdd, CancellationToken ct);
        Task<DetailEntity> Update(DetailEntity detailToUpdate, CancellationToken ct);
        Task<DetailEntity> Delete(Guid id, CancellationToken ct);
    }
}