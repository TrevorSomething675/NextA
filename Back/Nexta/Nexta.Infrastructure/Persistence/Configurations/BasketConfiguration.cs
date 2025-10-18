using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Basket;
using Nexta.Domain.Models.User;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserId)
                .IsRequired();

            builder.HasOne(typeof(BasketItem), "_products")
                .WithMany()
                .HasForeignKey("BasketId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(typeof(User), "Id")
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}