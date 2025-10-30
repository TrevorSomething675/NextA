using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Baskets;
using Nexta.Domain.Models.Users;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserId).IsRequired();

            builder.HasMany(b => b.Products)
                .WithOne()
                .HasForeignKey("BasketId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Basket>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}