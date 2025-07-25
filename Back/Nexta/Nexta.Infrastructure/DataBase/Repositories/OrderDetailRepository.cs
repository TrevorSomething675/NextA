﻿using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Exceptions;
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

		public async Task<OrderDetail> AddAsync(OrderDetail orderDetail, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetailEntity = _mapper.Map<OrderDetailEntity>(orderDetail);
				var createdOrderDetail = context.OrderDetails.Add(orderDetailEntity);
				await context.SaveChangesAsync(ct);

				var result = _mapper.Map<OrderDetail>(createdOrderDetail);

				return result;
			}
		}

		public async Task<OrderDetail> DeleteAsync(Guid orderId, Guid detailId, CancellationToken ct = default)
		{
			await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetailToDelete = await context.OrderDetails.FindAsync(orderId, detailId, ct);
				if (orderDetailToDelete == null) 
					throw new NotFoundException("Деталь не найдена в заказе");

				var deletedOrderDetailEntity = context.OrderDetails.Remove(orderDetailToDelete);
				await context.SaveChangesAsync(ct);
				var deletedOrderDetail = _mapper.Map<OrderDetail>(deletedOrderDetailEntity.Entity);

				return deletedOrderDetail;
			}
		}

		public async Task DeleteRangeAsync(List<OrderDetail> orderDetailsToDelete, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetailEntitiesToDelete = _mapper.Map<List<OrderDetailEntity>>(orderDetailsToDelete);

				context.OrderDetails.RemoveRange(orderDetailEntitiesToDelete);
				await context.SaveChangesAsync(ct);
			}
		}

		public async Task ReplaceOrderDetailAsync(Guid orderId, List<OrderDetail> orderDetails, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetailEntities = await context.OrderDetails
					.Where(od => od.OrderId == orderId).ToArrayAsync(ct);

				context.OrderDetails.RemoveRange(orderDetailEntities);
				await context.SaveChangesAsync(ct);

				var orderDetailEntitiesToAdd = _mapper.Map<List<OrderDetailEntity>>(orderDetails);
				context.OrderDetails.AddRange(orderDetailEntitiesToAdd);
				await context.SaveChangesAsync(ct);
			}
		}

		public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetailToUpdate, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var orderDetailEntity = await context.OrderDetails.FindAsync(orderDetailToUpdate.OrderId, orderDetailToUpdate.DetailId, ct);
				if (orderDetailEntity == null)
					throw new NotFoundException("Деталь не была найдена в заказе");

				orderDetailEntity.DetailId = orderDetailToUpdate.DetailId;
				orderDetailEntity.OrderId = orderDetailToUpdate.OrderId;
				orderDetailEntity.Count = orderDetailToUpdate.Count;

				await context.SaveChangesAsync(ct);
				return orderDetailToUpdate;
			}
		}
	}
}