using Nexta.Domain.Options;

namespace Nexta.Web.Extensions
{
	public static class SerivceCollectionOptionExtension
	{
		public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtAuthOptions>(configuration.GetSection(JwtAuthOptions.Section));
			services.Configure<DataBaseOptions>(configuration.GetSection(DataBaseOptions.Section));
			services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.Section));
			services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.Section));

			return services;
		}
	}
}