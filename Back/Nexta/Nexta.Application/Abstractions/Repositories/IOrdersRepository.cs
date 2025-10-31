using Nexta.Domain.Models.Orders;
using Nexta.Domain.Base;
using Nexta.Application.Abstractions.Specification;

namespace Nexta.Application.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<PagedData<Order>> GetAsync(ISpecification<Order> spec, CancellationToken ct = default);
        Task<PagedData<Order>> GetOrdersByFullNameAsync(ISpecification<Order> spec, CancellationToken ct = default);

        Order Update(Order order);
        Task<Order?> AddAsync(Order order, CancellationToken ct = default);
        Order Delete(Order order);
    }
}