using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<Guid> CreateOrderDetailAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default);
        Task AddRangeAsync(List<OrderDetail> orderDetailsToAdd, CancellationToken ct = default);
    }
}