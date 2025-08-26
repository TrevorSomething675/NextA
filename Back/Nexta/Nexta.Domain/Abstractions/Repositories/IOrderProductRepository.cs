using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderProductRepository
    {
        Task<OrderProduct> AddAsync(OrderProduct orderProduct, CancellationToken ct = default);
        Task AddRangeAsync(List<OrderProduct> orderProductToAdd, CancellationToken ct = default);

        Task<OrderProduct> UpdateAsync(OrderProduct orderDetailToUpdate, CancellationToken ct = default);
        Task<OrderProduct> DeleteAsync(Guid orderId, Guid productId, CancellationToken ct = default);
        Task DeleteRangeAsync(List<OrderProduct> orderProductToDelete, CancellationToken ct = default);
        Task ReplaceOrderProductAsync(Guid orderId, List<OrderProduct> orderProducts, CancellationToken ct = default);
    }
}
