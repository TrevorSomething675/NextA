using Microsoft.EntityFrameworkCore;
using Minio.DataModel.Notification;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Base;
using Nexta.Domain.Filters;
using Nexta.Domain.Models.Order;
using Nexta.Infrastructure.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly MainContext _context;

        public OrdersRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order, CancellationToken ct = default)
        {
            var createdOrder = await _context.Orders.AddAsync(order, ct);
            return createdOrder.Entity;
        }

        public Order Delete(Order order)
        {
            var result = _context.Orders.Remove(order);
            return result.Entity;
        }

        public async Task<Order?> GetAsync(Guid id, CancellationToken ct = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, ct);
            return order;
        }

        public async Task<Order?> GetAsyncByUserId(Guid userId, CancellationToken ct = default)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userId, ct);
            return order;
        }

        public async Task<PagedData<Order>> GetOrdersByFullNameAsync(GetOrdersFilter filter, CancellationToken ct = default)
        {
            var searchTerm = filter.SearchTerm?.ToLower() ?? "";

            var query = _context.Orders
                .Include(o => o.Products)
                .Where(o => _context.Users
                    .Where(u =>
                        EF.Functions.Like(u.Email.ToLower(), searchTerm) ||
                        EF.Functions.Like(u.FirstName.ToLower(), searchTerm) ||
                        EF.Functions.Like(u.MiddleName.ToLower(), searchTerm) ||
                        EF.Functions.Like(u.LastName.ToLower(), searchTerm)
                    )
                .Select(u => u.Id)
                .Contains(o.UserId));

            var orders = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            var ordersCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

            var pagedOrders = new PagedData<Order>(orders, orders.Count, pageCount);

            return pagedOrders;
        }

        public async Task<PagedData<Order>> GetPagedAsync(GetOrdersFilter filter, CancellationToken ct = default)
        {
            var query = _context.Orders
                .Include(o => o.Products)
                .Where(o => filter.UserId == default || o.UserId == filter.UserId)
                .Where(o => filter.Statuses.Contains(o.Status));

            var orders = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            var ordersCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

            var pagedOrders = new PagedData<Order>(orders, orders.Count, pageCount);

            return pagedOrders;
        }

        public Order Update(Order order)
        {
            var result = _context.Orders.Update(order);
            return result.Entity;
        }
    }
}
