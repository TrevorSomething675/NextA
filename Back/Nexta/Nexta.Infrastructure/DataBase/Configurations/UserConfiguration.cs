using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}