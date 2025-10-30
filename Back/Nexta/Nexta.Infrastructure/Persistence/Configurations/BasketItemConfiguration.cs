using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Baskets;
using Nexta.Domain.Models.Products;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Count)
                .IsRequired();

            builder.Property(b => b.ProductId)
                .IsRequired();

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}