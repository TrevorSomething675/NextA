using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProductEntity>
    {
        public void Configure(EntityTypeBuilder<OrderProductEntity> builder)
        {
            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(od => od.OrderId);

            builder.HasOne(od => od.Product)
                .WithMany(d => d.OrderProducts)
                .HasForeignKey(od => od.ProductId);

            builder.HasKey(od => new { od.OrderId, od.ProductId });
        }
    }
}