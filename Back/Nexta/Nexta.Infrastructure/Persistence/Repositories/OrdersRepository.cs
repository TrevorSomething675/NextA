using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Filters;
using Nexta.Domain.Base;
using Nexta.Domain.Specification.Abstractions;

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

        public async Task<PagedData<Order>> GetOrdersByFullNameAsync(ISpecification<Order> spec, CancellationToken ct = default)
        {
            var query = _context.Orders
                .Include(o => o.Products)
                .Where(o => _context.Users
                    .Where(u => !string.IsNullOrWhiteSpace(spec.SearchTerm) ? 
                        EF.Functions.Like(u.Email.ToLower(), spec.SearchTerm) ||
                        EF.Functions.Like(u.FirstName.ToLower(), spec.SearchTerm) ||
                        EF.Functions.Like(u.MiddleName.ToLower(), spec.SearchTerm) ||
                        EF.Functions.Like(u.LastName.ToLower(), spec.SearchTerm) : true
                    )
                .Select(u => u.Id)
                .Contains(o.UserId));

            var orders = await query
                .Skip((spec.PageNumber - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync(ct);

            var ordersCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)ordersCount / spec.PageSize);

            var pagedOrders = new PagedData<Order>(orders, orders.Count, pageCount);

            return pagedOrders;
        }

        public async Task<PagedData<Order>> GetPagedAsync(ISpecification<Order> spec, CancellationToken ct = default)
        {
            var query = _context.Orders
                .Include(o => spec.Includes)
                .Where(spec.Creteria);

            var orders = await query
                .Skip((spec.PageNumber - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync(ct);

            var ordersCount = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)ordersCount / spec.PageSize);

            var pagedOrders = new PagedData<Order>(orders, orders.Count, pageCount);

            return pagedOrders;
        }

        public Order Update(Order order)
        {
            var result = _context.Orders.Update(order);
            return result.Entity;
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _context.Orders.CountAsync(ct);
        }
    }
}
