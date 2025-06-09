using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetailEntity>> CreateOrderDetailAsync(Guid userId, List<Guid> detailIds, CancellationToken ct = default);
        Task<List<OrderDetailEntity>> AddRangeAsync(List<OrderDetailEntity> orderDetailsToAdd, CancellationToken ct = default);
    }
}