using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class DetailConfiguration : IEntityTypeConfiguration<DetailEntity>
	{
		public void Configure(EntityTypeBuilder<DetailEntity> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}