using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Product;
using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired().HasMaxLength(100);

            builder.Property(p => p.Article)
                .IsRequired().HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Category)
                .IsRequired(false).HasMaxLength(100);

            builder.Property(p => p.Count)
                .IsRequired();

            builder.Property(p => p.NewPrice)
                .IsRequired();

            builder.Property(p => p.OldPrice)
                .IsRequired(false);

            builder.HasMany(typeof(ProductImage), "_images")
                .WithOne()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(typeof(ProductAttribute), "_attributes")
                .WithOne()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");

            builder.HasIndex(p => p.IsVisible)
                .HasDatabaseName("IX_Products_IsVisible");

            builder.Navigation("_attributes").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation("_images").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}