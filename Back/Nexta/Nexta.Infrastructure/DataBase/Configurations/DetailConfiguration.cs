using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class DetailConfiguration : IEntityTypeConfiguration<DetailEntity>
	{
		public void Configure(EntityTypeBuilder<DetailEntity> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(d => d.Image)
				.WithOne(i => i.Detail)
				.HasForeignKey<DetailImageEntity>(i => i.DetailId);
		}
	}
}