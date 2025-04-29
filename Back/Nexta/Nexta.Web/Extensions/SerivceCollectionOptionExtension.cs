using Nexta.Domain.Options;

namespace Nexta.Web.Extensions
{
	public static class SerivceCollectionOptionExtension
	{
		public static IServiceCollection AddAppOptions(this IServiceCollection services)
		{
			var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
			services.Configure<JwtAuthOptions>(configuration.GetSection(JwtAuthOptions.Section));
			services.Configure<DataBaseOptions>(configuration.GetSection(DataBaseOptions.Section));

			return services;
		}
	}
}