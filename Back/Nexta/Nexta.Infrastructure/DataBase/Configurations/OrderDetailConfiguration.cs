using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
	{
		public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
		{
			builder.HasOne(od => od.Order)
				.WithMany(o => o.OrderDetails)
				.HasForeignKey(od => od.OrderId);

			builder.HasOne(od => od.Detail)
				.WithMany(d => d.OrderDetails)
				.HasForeignKey(od => od.DetailId);

			builder.HasKey(od => new { od.OrderId, od.DetailId });
		}
	}
}