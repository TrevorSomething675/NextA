using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
    internal class BasketProductConfiguration : IEntityTypeConfiguration<BasketProductEntity>
    {
        public void Configure(EntityTypeBuilder<BasketProductEntity> builder)
        {
            builder.HasOne(ud => ud.User)
                .WithMany(u => u.BasketProducts)
                .HasForeignKey(ud => ud.UserId);

            builder.HasOne(ud => ud.Product)
                .WithMany(d => d.BasketProducts)
                .HasForeignKey(ud => ud.ProductId);

            builder.HasKey(ud => new { ud.UserId, ud.ProductId});
        }
    }
}