using Nexta.Application.Abstractions.Repositories;

namespace Nexta.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IBasketRepository Baskets { get; }
        ICategoriesRepository Categories { get; }
        INewsRepository News { get; }
        INotificationsRepository Notifications { get; }
        IOrdersRepository Orders { get; }
        IProductsRepository Products { get; }
        IUsersRepository Users { get; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);
    }
}
