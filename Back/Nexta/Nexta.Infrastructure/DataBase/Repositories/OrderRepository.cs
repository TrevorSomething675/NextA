using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;
using Nexta.Domain.Filters;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public OrderRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<OrderEntity> AddAsync(OrderEntity orderToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var createdOrder = (await context.Orders.AddAsync(orderToAdd, ct)).Entity;
				await context.SaveChangesAsync();

				return createdOrder;
			}
		}

		public async Task<int> CountOrdersAsync(Guid userId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var count = await context.Orders
					.Where(o => o.UserId == userId)
					.CountAsync(ct);

				return count;
			}
		}

		public async Task<PagedData<OrderEntity>> GetLegacyOrdersAsync(OrdersFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Orders
					.Where(o => o.UserId == filter.UserId)
					.Where(o => 
						o.Status == Domain.Enums.OrderStatus.Canceled || 
						o.Status == Domain.Enums.OrderStatus.Complete)
					.AsNoTracking();

				var orders = await query
					.Include(o => o.OrderDetails)!.ThenInclude(od => od.Detail)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var ordersCount = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

				return new PagedData<OrderEntity>(orders, orders.Count, pageCount);
			}
		}

		public async Task<OrderEntity> GetOrderAsync(Guid orderId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var order = await context.Orders
					.AsNoTracking()
					.Include(o => o.OrderDetails)
					.Include(o => o.Details)
					.FirstOrDefaultAsync(ct);

				return order;
			}
		}

		public async Task<PagedData<OrderEntity>> GetOrdersAsync(OrdersFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Orders
					.Where(o => o.UserId == filter.UserId)
					.Where(o =>
						o.Status != Domain.Enums.OrderStatus.Canceled &&
						o.Status != Domain.Enums.OrderStatus.Complete)
					.AsNoTracking();

				var orders = await query
					.Include(o => o.OrderDetails)!.ThenInclude(od => od.Detail)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var ordersCount = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

				return new PagedData<OrderEntity>(orders, orders.Count, pageCount);
			}
		}
	}
}