using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Image)
                .WithOne(i => i.Product)
                .HasForeignKey<ProductImageEntity>(i => i.ProductId);

            builder.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");

            builder.HasIndex(p => p.IsVisible)
                .HasDatabaseName("IX_Products_IsVisible");

            builder.HasMany(p => p.Attributes)
                .WithOne(a => a.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}