using Nexta.Infrastructure.DataBase.Repositories;
using Nexta.Domain.Abstractions.Repositories;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionRepositoryExtension
	{
		public static  IServiceCollection AddAppRepositories(this IServiceCollection services)
		{
			services
				.AddScoped<IUserRepository, UserRepository>()
				.AddScoped<IProductRepository, ProductsRepository>()
				.AddScoped<IBasketProductRepository, BasketProductRepository>()
				.AddScoped<IOrderRepository, OrderRepository>()
				.AddScoped<IOrderProductRepository, OrderProductRepository>()
				.AddScoped<INewsImageRepository, NewsImageRepository>()
				.AddScoped<IProductImageRepository, ProductImageRepository>()
				.AddScoped<INewsRepository, NewsRepository>();

			return services;
		}
	}
}