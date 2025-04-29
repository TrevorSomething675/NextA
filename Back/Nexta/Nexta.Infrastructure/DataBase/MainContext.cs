using Nexta.Infrastructure.DataBase.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nexta.Domain.Entities;
using Nexta.Domain.Options;

namespace Nexta.Infrastructure.DataBase
{
	public class MainContext : DbContext
	{
		private readonly DataBaseOptions _options;

		public DbSet<DetailEntity> Details { get; set; }
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<UserDetailEntity> UserDetails { get; set; }

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
		}
	}
}