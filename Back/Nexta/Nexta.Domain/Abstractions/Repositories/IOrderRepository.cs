using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderEntity> GetOrderAsync(Guid orderId, CancellationToken ct = default);
        Task<PagedData<OrderEntity>> GetOrdersAsync(GetOrdersFilter filter, CancellationToken ct = default);
        Task<PagedData<OrderEntity>> GetAllOrdersAsync(GetAllOrdersFilter filter, CancellationToken ct = default);

        Task<OrderEntity> AddAsync(OrderEntity orderToAdd, CancellationToken ct = default);

        Task<int> CountOrdersAsync(Guid userId, CancellationToken ct = default);
	}
}