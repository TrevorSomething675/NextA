using Nexta.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nexta.Domain.Models.Basket;
using Nexta.Domain.Models.News;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Models.Product;
using Nexta.Domain.Models.User;
using Nexta.Domain.Options;

namespace Nexta.Infrastructure.Persistence
{
	public class MainContext : DbContext
	{
		private readonly DataBaseOptions _options;

		public DbSet<User> Users { get; set; }
		public DbSet<Basket> Basket { get; set; }

		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderProducts { get; set; }

		public DbSet<News> News { get; set; }

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<ProductAttribute> ProductAttributes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Notification> Notifications { get; set; }

		public MainContext(IOptions<DataBaseOptions> options)
		{
			_options = options.Value;
		}

		public void BeginTransactionAsync(CancellationToken ct = default)
		{
			Database.BeginTransactionAsync(ct);
        }

		public void CommitTransactionAsync(CancellationToken ct = default)
		{
			Database.CommitTransactionAsync(ct);
		}

		public void RollbackTransactionAsync(CancellationToken ct = default)
		{
			Database.RollbackTransactionAsync(ct);
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