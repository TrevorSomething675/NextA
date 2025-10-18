using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Nexta.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;
        private IDbContextTransaction? _transaction;
        
        public UnitOfWork(MainContext context, 
            IOrdersRepository orders,
            IBasketRepository baskets,
            ICategoriesRepository categories,
            INewsRepository news,
            INotificationsRepository notifications,
            IProductsRepository products,
            IUsersRepository users
        )
        {
            _context = context;
            Orders = orders;
            Baskets = baskets;
            Categories = categories;
            News = news;
            Notifications = notifications;
            Products = products;
            Users = users;
        }

        public IOrdersRepository Orders { get; }
        public IBasketRepository Baskets { get; }
        public ICategoriesRepository Categories { get; }
        public INewsRepository News { get; }
        public INotificationsRepository Notifications { get; }
        public IProductsRepository Products { get; }
        public IUsersRepository Users { get; }

        public async Task BeginTransactionAsync(CancellationToken ct = default)
        {
            if (_transaction != null)
                return;

            _transaction = await _context.Database.BeginTransactionAsync(ct);
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch
            {
                await RollbackAsync(ct);
                throw;
            }
            finally
            {
                if(_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackAsync(CancellationToken ct = default)
        {
            if(_transaction != null)
            {
                await _transaction.RollbackAsync(ct);
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
