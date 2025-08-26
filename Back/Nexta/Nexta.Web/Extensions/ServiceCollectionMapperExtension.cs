using System.Reflection;
using AutoMapper;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionMapperExtension
	{
		public static IServiceCollection AddAppMapper(this IServiceCollection services)
		{
			var mapperConfig = new MapperConfiguration(config =>
			{
				config.AddMaps(Assembly.GetAssembly(typeof(Application.AssemblyMarker)));
				config.AddMaps(Assembly.GetAssembly(typeof(Infrastructure.AssemblyMarker)));
				config.AddMaps(Assembly.GetAssembly(typeof(Web.AssemblyMarker)));
			});
			//mapperConfig.AssertConfigurationIsValid();
			var mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			return services;
		}
	}
}