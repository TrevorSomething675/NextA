using Nexta.Infrastructure.DataBase.Configurations;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nexta.Domain.Options;

namespace Nexta.Infrastructure.DataBase
{
	public class MainContext : DbContext
	{
		private readonly DataBaseOptions _options;

		public DbSet<UserEntity> Users { get; set; }
		public DbSet<BasketProductEntity> BasketProducts { get; set; }

		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<OrderProductEntity> OrderProducts { get; set; }

		public DbSet<NewsEntity> News { get; set; }
		public DbSet<NewsImageEntity> NewsImages { get; set; }

		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<ProductImageEntity> ProductImages { get; set; }

		public MainContext(IOptions<DataBaseOptions> options)
		{
			_options = options.Value;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(_options.ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new NewsConfiguration());
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new BasketProductConfiguration());
			modelBuilder.ApplyConfiguration(new OrderProductConfiguration());

		}
	}
}