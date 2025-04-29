using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Configurations
{
	public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetailEntity>
	{
		public void Configure(EntityTypeBuilder<UserDetailEntity> builder)
		{
			builder.HasOne(ud => ud.User)
				.WithMany(u => u.UserDetail)
				.HasForeignKey(ud => ud.UserId);

			builder.HasOne(ud => ud.Detail)
				.WithMany(d => d.UserDetail)
				.HasForeignKey(ud => ud.DetailId);

			builder.HasKey(ud => new { ud.UserId, ud.DetailId });
		}
	}
}