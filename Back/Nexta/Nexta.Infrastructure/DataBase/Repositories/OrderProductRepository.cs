using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using Nexta.Infrastructure.DataBase.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public OrderProductRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<OrderProduct> AddAsync(OrderProduct orderProduct, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductEntity = _mapper.Map<OrderProductEntity>(orderProduct);
                var createdOrderProduct = context.OrderProducts.Add(orderProductEntity);
                await context.SaveChangesAsync(ct);

                var result = _mapper.Map<OrderProduct>(createdOrderProduct);

                return result;
            }
        }

        public async Task AddRangeAsync(List<OrderProduct> orderProductToAdd, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductEntities = _mapper.Map<List<OrderProductEntity>>(orderProductToAdd);
                await context.OrderProducts.AddRangeAsync(orderProductEntities, ct);
                await context.SaveChangesAsync(ct);
            }
        }

        public async Task<OrderProduct> DeleteAsync(Guid orderId, Guid productId, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductToDelete = await context.OrderProducts.FindAsync(orderId, productId, ct);
                if (orderProductToDelete == null)
                    throw new NotFoundException("Деталь не найдена в заказе");

                var deletedOrderProductEntity = context.OrderProducts.Remove(orderProductToDelete);
                await context.SaveChangesAsync(ct);
                var deletedOrderProduct = _mapper.Map<OrderProduct>(deletedOrderProductEntity.Entity);

                return deletedOrderProduct;
            }
        }

        public async Task DeleteRangeAsync(List<OrderProduct> orderProductToDelete, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductEntitiesToDelete = _mapper.Map<List<OrderProductEntity>>(orderProductToDelete);

                context.OrderProducts.RemoveRange(orderProductEntitiesToDelete);
                await context.SaveChangesAsync(ct);
            }
        }

        public async Task ReplaceOrderProductAsync(Guid orderId, List<OrderProduct> orderProducts, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductEntities = await context.OrderProducts
                    .Where(od => od.OrderId == orderId).ToArrayAsync(ct);

                context.OrderProducts.RemoveRange(orderProductEntities);
                await context.SaveChangesAsync(ct);

                var orderProductEntitiesToAdd = _mapper.Map<List<OrderProductEntity>>(orderProducts);
                context.OrderProducts.AddRange(orderProductEntitiesToAdd);
                await context.SaveChangesAsync(ct);
            }
        }

        public async Task<OrderProduct> UpdateAsync(OrderProduct orderProductToUpdate, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var orderProductEntity = await context.OrderProducts.FindAsync(orderProductToUpdate.OrderId, orderProductToUpdate.ProductId, ct);
                if (orderProductEntity == null)
                    throw new NotFoundException("Деталь не была найдена в заказе");

                orderProductEntity.ProductId = orderProductToUpdate.ProductId;
                orderProductEntity.OrderId = orderProductToUpdate.OrderId;
                orderProductEntity.Count = orderProductToUpdate.Count;

                await context.SaveChangesAsync(ct);
                return orderProductToUpdate;
            }
        }
    }
}