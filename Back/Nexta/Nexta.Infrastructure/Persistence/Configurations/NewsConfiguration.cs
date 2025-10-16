using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Infrastructure.Persistence.Entities;

namespace Nexta.Infrastructure.Persistence.Configurations
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
