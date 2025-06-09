using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<PagedData<OrderEntity>> GetOrdersAsync(OrdersFilter filter, CancellationToken ct = default);
        Task<PagedData<OrderEntity>> GetLegacyOrdersAsync(OrdersFilter filter, CancellationToken ct = default);

        Task<OrderEntity> GetOrderAsync(Guid orderId, CancellationToken ct = default);

        Task<OrderEntity> AddAsync(OrderEntity orderToAdd, CancellationToken ct = default);

        Task<int> CountOrdersAsync(Guid userId, CancellationToken ct = default);
	}
}