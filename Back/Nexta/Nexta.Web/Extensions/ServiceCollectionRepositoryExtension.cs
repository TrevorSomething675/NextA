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
				.AddScoped<IDetailRepository, DetailRepository>()
				.AddScoped<IUserDetailRepository, UserDetailRepository>()
				.AddScoped<IOrderRepository, OrderRepository>()
				.AddScoped<IOrderDetailRepository, OrderDetailRepository>()
				.AddScoped<INewsImageRepository, NewsImageRepository>()
				.AddScoped<INewsRepository, NewsRepository>();

			return services;
		}
	}
}