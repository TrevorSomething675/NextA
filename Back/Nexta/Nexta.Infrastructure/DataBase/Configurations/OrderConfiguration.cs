using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);

			builder.HasMany(o => o.Details)
				.WithMany(d => d.Orders)
				.UsingEntity<OrderDetailEntity>(
					e => e.HasOne(od => od.Detail)
						.WithMany(d => d.OrderDetails)
						.HasForeignKey(od => od.DetailId),
					e => e.HasOne(od => od.Order)
						.WithMany(o => o.OrderDetails)
						.HasForeignKey(od => od.OrderId),
					e => e.HasKey(od => new { od.OrderId, od.DetailId })
				);
		}
	}
}