using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class OrderDetailRepository : IOrderDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;

		public OrderDetailRepository(IDbContextFactory<MainContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory;
		}

		public async Task<List<OrderDetailEntity>> AddRangeAsync(List<OrderDetailEntity> orderDetailsToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				await context.OrderDetails.AddRangeAsync(orderDetailsToAdd, ct);
				await context.SaveChangesAsync(ct);

				return orderDetailsToAdd;
			}
		}

		public async Task<List<OrderDetailEntity>> CreateOrderDetailAsync(Guid orderId, List<Guid> detailIds, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetails = new List<OrderDetailEntity>();
				foreach (var detailId in detailIds)
				{
					var order = new OrderDetailEntity
					{
						OrderId = orderId,
						DetailId = detailId
					};
					orderDetails.Add(order);
				}
				context.OrderDetails.AddRange(orderDetails);
				await context.SaveChangesAsync(ct);

				var createdOrderDetails = await context.OrderDetails
					.AsNoTracking()
					.Where(od => od.OrderId == orderId && detailIds.Contains(od.DetailId))
					.Include(od => od.Detail)
					.ToListAsync();

				return createdOrderDetails;
			}
		}
	}
}