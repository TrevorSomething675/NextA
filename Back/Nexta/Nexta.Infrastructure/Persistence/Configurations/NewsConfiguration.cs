using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.News;

namespace Nexta.Infrastructure.Persistence.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Base64String)
                .IsRequired();

            builder.Property(n => n.Header)
                .IsRequired(false);

            builder.Property(n => n.Description)
                .IsRequired(false);
        }
    }
}
