using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<Guid> CreateOrderDetailAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default);
        Task AddRangeAsync(List<OrderDetail> orderDetailsToAdd, CancellationToken ct = default);

        Task<OrderDetail> UpdateAsync(OrderDetail orderDetailToUpdate, CancellationToken ct = default);
        Task<OrderDetail> DeleteAsync(Guid orderId, Guid detailId, CancellationToken ct = default);
        Task DeleteRangeAsync(List<OrderDetail> orderDetailsToDelete, CancellationToken ct = default);
        Task ReplaceOrderDetailAsync(Guid orderId, List<OrderDetail> orderDetails, CancellationToken ct = default);
    }
}