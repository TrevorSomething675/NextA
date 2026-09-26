using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderAsync(Guid orderId, CancellationToken ct = default);
        Task<PagedData<Order>> GetOrdersAsync(GetOrdersFilter filter, CancellationToken ct = default);
        //Task<PagedData<Order>> GetAllOrdersAsync(GetAllOrdersFilter filter, CancellationToken ct = default);

        Task<Order> AddAsync(Order orderToAdd, CancellationToken ct = default);
        Task<Guid> UpdateAsync(Order order, CancellationToken ct = default);
        Task<Guid> DeleteAsync(Guid orderId, CancellationToken ct = default);
        Task<int> CountOrdersAsync(Guid userId, CancellationToken ct = default);
	}
}