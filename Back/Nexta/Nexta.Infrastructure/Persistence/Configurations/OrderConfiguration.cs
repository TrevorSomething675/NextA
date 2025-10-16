using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Infrastructure.Persistence.Entities;

namespace Nexta.Infrastructure.Persistence.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);

			builder.HasMany(o => o.Products)
				.WithMany(d => d.Orders)
				.UsingEntity<OrderProductEntity>(
					e => e.HasOne(od => od.Product)
						.WithMany(d => d.OrderProducts)
						.HasForeignKey(od => od.ProductId),
					e => e.HasOne(od => od.Order)
						.WithMany(o => o.OrderProducts)
						.HasForeignKey(od => od.OrderId),
					e => e.HasKey(od => new { od.OrderId, od.ProductId})
				);
		}
	}
}