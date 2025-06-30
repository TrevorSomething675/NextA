using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class OrderDetailRepository : IOrderDetailRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;

		public OrderDetailRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public async Task AddRangeAsync(List<OrderDetail> orderDetailsToAdd, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var ordeDetailEntities = _mapper.Map<List<OrderDetailEntity>>(orderDetailsToAdd);
				await context.OrderDetails.AddRangeAsync(ordeDetailEntities, ct);
				await context.SaveChangesAsync(ct);
			}
		}

		public async Task<Guid> CreateOrderDetailAsync(Guid orderId, List<Guid> detailIds, CancellationToken ct = default)
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

				return orderId;
			}
		}
	}
}