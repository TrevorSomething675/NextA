using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Persistence.Repositories;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionRepositoryExtension
	{
		public static  IServiceCollection AddAppRepositories(this IServiceCollection services)
		{
			services
				.AddScoped<IUsersRepository, UsersRepository>()
				.AddScoped<IProductsRepositoryL, ProductsRepositoryL>()
				.AddScoped<IBasketProductRepository, BasketProductRepository>()
				.AddScoped<IOrderRepositoryL, OrderRepositoryL>()
				.AddScoped<IOrderProductRepositoryL, OrderProductRepositoryL>()
				.AddScoped<IProductImageRepository, ProductImageRepository>()
				.AddScoped<INotificationsRepository, NotificationsRepository>()
				.AddScoped<ICategoriesRepository, CategoriesRepository>()
				.AddScoped<INewsRepository, NewsRepository>();

			return services;
		}
	}
}