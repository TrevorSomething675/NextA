using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Products;
using Nexta.Domain.Models.Orders;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Count)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}