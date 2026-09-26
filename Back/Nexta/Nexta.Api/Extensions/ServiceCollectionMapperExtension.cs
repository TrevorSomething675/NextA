namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionMapperExtension
	{
		public static IServiceCollection AddAppMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(config =>
			{
				config.AddMaps(
					typeof(Application.AssemblyMarker).Assembly,
					typeof(Infrastructure.AssemblyMarker).Assembly,
					typeof(Web.AssemblyMarker).Assembly);
			});

			return services;
		}
	}
}