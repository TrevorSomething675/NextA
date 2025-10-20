using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Product;

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

            builder.Property(p => p.IsVisible)
                .HasDefaultValue(false);

            builder.Property(p => p.Count)
                .IsRequired();

            builder.Property(p => p.NewPrice)
                .IsRequired();

            builder.Property(p => p.OldPrice)
                .IsRequired(false);

            builder.HasIndex(p => p.Category)
                .HasDatabaseName("IX_Products_Category");

            builder.HasIndex(p => p.IsVisible)
                .HasDatabaseName("IX_Products_IsVisible");

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(p => p.Images)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(p => p.Attributes)
                .WithOne()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(p => p.Attributes)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}