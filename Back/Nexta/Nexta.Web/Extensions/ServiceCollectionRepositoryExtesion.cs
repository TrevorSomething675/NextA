using Nexta.Infrastructure.DataBase.Repositories;
using Nexta.Domain.Abstractions.Repositories;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionRepositoryExtesion
	{
		public static  IServiceCollection AddAppRepositories(this IServiceCollection services)
		{
			services
				.AddScoped<IUserRepository, UserRepository>()
				.AddScoped<IDetailRepository, DetailRepository>()
				.AddScoped<IUserDetailRepository, UserDetailRepository>();

			return services;
		}
	}
}