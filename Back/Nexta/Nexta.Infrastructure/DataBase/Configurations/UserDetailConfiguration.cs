using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetailEntity>
	{
		public void Configure(EntityTypeBuilder<UserDetailEntity> builder)
		{
			builder.HasOne(ud => ud.User)
				.WithMany(u => u.UserDetails)
				.HasForeignKey(ud => ud.UserId);

			builder.HasOne(ud => ud.Detail)
				.WithMany(d => d.UserDetails)
				.HasForeignKey(ud => ud.DetailId);

			builder.HasKey(ud => new { ud.UserId, ud.DetailId });
		}
	}
}