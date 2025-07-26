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

		public DbSet<DetailEntity> Details { get; set; }
		public DbSet<DetailImageEntity> DetailImages { get; set; }
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<UserDetailEntity> UserDetails { get; set; }
		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<OrderDetailEntity> OrderDetails { get; set; }
		public DbSet<NewsEntity> News { get; set; }
		public DbSet<NewsImageEntity> NewsImages { get; set; }

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
			modelBuilder.ApplyConfiguration(new DetailConfiguration());
			modelBuilder.ApplyConfiguration(new UserDetailConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
			modelBuilder.ApplyConfiguration(new NewsConfiguration());
		}
	}
}