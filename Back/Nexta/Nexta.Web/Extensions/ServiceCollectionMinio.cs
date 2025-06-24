using Nexta.Domain.Abstractions.Services;
using Nexta.Infrastructure.Minio;
using Minio.DataModel.Args;
using Nexta.Domain.Options;
using Minio;

namespace Nexta.Web.Extensions
{
	public static class ServiceCollectionMinio
	{
		public static async Task<IServiceCollection> AddAppMinio(this IServiceCollection services, IConfiguration configuration)
		{
			var options = configuration.GetSection(MinioOptions.Section).Get<MinioOptions>();

			var minioClient = new MinioClient()
				.WithEndpoint(options.Endpoint)
				.WithCredentials(options.AccessKey, options.SecretKey)
				.Build();

			var isNewsBucketExist = await minioClient.BucketExistsAsync(
				new BucketExistsArgs().WithBucket("news"));

			if (!isNewsBucketExist)
			{
				await minioClient.MakeBucketAsync(
					new MakeBucketArgs().WithBucket("news"));
			}

			services.AddSingleton<IMinioClient>(minioClient);
			services.AddScoped<IMinioService, MinioService>();

			return services;
		}
	}
}