using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<NewsEntity>
    {
        public void Configure(EntityTypeBuilder<NewsEntity> builder)
        {
            builder.HasOne(n => n.Image)
                .WithOne(i => i.News)
                .HasForeignKey<NewsImageEntity>(i => i.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
