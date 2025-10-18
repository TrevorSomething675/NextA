using Nexta.Domain.Base;
using Nexta.Domain.Filters;
using Nexta.Domain.Models.Order;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order?> GetAsync(Guid id, CancellationToken ct = default);
        Task<Order?> GetAsyncByUserId(Guid userId, CancellationToken ct = default);
        Task<PagedData<Order>> GetPagedAsync(GetOrdersFilter filter, CancellationToken ct = default);
        Task<PagedData<Order>> GetOrdersByFullNameAsync(GetOrdersFilter filter, CancellationToken ct = default);

        Order Update(Order order);
        Task<Order?> AddAsync(Order order, CancellationToken ct = default);
        Order Delete(Order order);
    }
}