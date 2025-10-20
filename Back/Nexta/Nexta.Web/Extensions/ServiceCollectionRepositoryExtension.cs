using Nexta.Infrastructure.Persistence.Repositories;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Persistence;
using Nexta.Domain.Abstractions;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionRepositoryExtension
	{
		public static  IServiceCollection AddAppRepositories(this IServiceCollection services)
		{
			services
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped<IOrdersRepository, OrdersRepository>()
				.AddScoped<IBasketRepository, BasketRepository>()
				.AddScoped<ICategoriesRepository, CategoriesRepository>()
				.AddScoped<INewsRepository, NewsRepository>()
				.AddScoped<INotificationsRepository, NotificationsRepository>()
				.AddScoped<IProductsRepository, ProductsRepository>()
				.AddScoped<IUsersRepository, UsersRepository>();

			return services;
		}
	}
}