using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order?> GetAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<Order>> GetPagedAsync(ISpecification<Order> spec, CancellationToken ct = default);
        Task<PagedData<Order>> GetOrdersByFullNameAsync(ISpecification<Order> spec, CancellationToken ct = default);

        Order Update(Order order);
        Task<Order?> AddAsync(Order order, CancellationToken ct = default);
        Order Delete(Order order);

        Task<int> CountAsync(CancellationToken ct);
    }
}