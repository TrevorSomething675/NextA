using Nexta.Infrastructure.DataBase.Repositories;
using Nexta.Domain.Abstractions.Repositories;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionRepositoryExtension
	{
		public static  IServiceCollection AddAppRepositories(this IServiceCollection services)
		{
			services
				.AddScoped<IUsersRepository, UsersRepository>()
				.AddScoped<IProductsRepository, ProductsRepository>()
				.AddScoped<IBasketProductRepository, BasketProductRepository>()
				.AddScoped<IOrderRepository, OrderRepository>()
				.AddScoped<IOrderProductRepository, OrderProductRepository>()
				.AddScoped<INewsImageRepository, NewsImageRepository>()
				.AddScoped<IProductImageRepository, ProductImageRepository>()
				.AddScoped<INotificationsRepository, NotificationsRepository>()
				.AddScoped<ICategoriesRepository, CategoriesRepository>()
				.AddScoped<INewsRepository, NewsRepository>();

			return services;
		}
	}
}