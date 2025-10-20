using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Models.User;

namespace Nexta.Infrastructure.Persistence.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.Id);

			builder.Property(o => o.Status)
				.HasConversion<string>()
				.IsRequired();

			builder.Property(o => o.CreatedDate)
				.IsRequired();

			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey("UserId")
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(o => o.Products)
				.WithOne()
				.HasForeignKey("OrderId")
				.OnDelete(DeleteBehavior.Cascade);

			builder.Navigation(o => o.Products)
				.UsePropertyAccessMode(PropertyAccessMode.Field);
		}
	}
}