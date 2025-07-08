using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Filters;
using Nexta.Domain.Models;
using AutoMapper;
using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Exceptions;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;

		public OrderRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public async Task<Order> AddAsync(Order orderToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderEntity = _mapper.Map<OrderEntity>(orderToAdd);
				var createdOrderEntity = (await context.Orders.AddAsync(orderEntity, ct)).Entity;
				await context.SaveChangesAsync();

				var createdOrder = _mapper.Map<Order>(createdOrderEntity);

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

		public async Task<Order> GetOrderAsync(Guid orderId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderEntity = await context.Orders
					.AsNoTracking()
					.Include(o => o.OrderDetails)
					.Include(o => o.Details)
					.FirstOrDefaultAsync(ct);

				var order = _mapper.Map<Order>(orderEntity);

				return order;
			}
		}

		public async Task<PagedData<Order>> GetAllOrdersAsync(GetAllOrdersFilter filter, CancellationToken ct = default)
		{
			await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Orders
					.AsNoTracking()
					.Include(o => o.User)
					.Where(o => filter.Statuses.Contains(o.Status));

				var orders = await query
					.Include(o => o.OrderDetails)!.ThenInclude(od => od.Detail)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var ordersCount = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

				var pagedOrderEntities = new PagedData<OrderEntity>(orders, ordersCount, pageCount);

				var pagedOrders = _mapper.Map<PagedData<Order>>(pagedOrderEntities);

				return pagedOrders;
			}
		}

		public async Task<PagedData<Order>> GetOrdersAsync(GetOrdersFilter filter, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Orders
					.AsNoTracking()
					.Where(o => o.UserId == filter.UserId)
					.Where(o => filter.Statuses.Contains(o.Status))
					.AsNoTracking();

				var orders = await query
					.Include(o => o.OrderDetails)!.ThenInclude(od => od.Detail)
					.Skip((filter.PageNumber - 1) * filter.PageSize)
					.Take(filter.PageSize)
					.ToListAsync(ct);

				var ordersCount = await query.CountAsync(ct);
				var pageCount = (int)Math.Ceiling((double)ordersCount / filter.PageSize);

				var pagedOrderEntities = new PagedData<OrderEntity>(orders, orders.Count, pageCount);
				
				var pagedOrders = _mapper.Map<PagedData<Order>>(pagedOrderEntities);

				return pagedOrders;
			}
		}

		public async Task<Guid> UpdateAsync(Order order, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderEntity = await context.Orders
					.FirstOrDefaultAsync(o => o.Id == order.Id, ct);

				if (orderEntity == null) throw new NotFoundException("Заказ не найден");

				orderEntity.Status = order.Status;

				await context.SaveChangesAsync(ct);

				return orderEntity.Id;
			}
		}

		public async Task<Guid> DeleteAsync(Guid orderId, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var order = await context.Orders.FindAsync(orderId, ct);
				if (order == null)
					throw new NotFoundException("Заказ не найден");

				var deletedEntity = context.Orders.Remove(order);
				await context.SaveChangesAsync(ct);

				return deletedEntity.Entity.Id;
			}
		}
	}
}