using Nexta.Domain.Abstractions.Services;
using Minio.DataModel.Args;
using Nexta.Domain.Models;
using Minio;
using Minio.Exceptions;
using Microsoft.Extensions.Logging;

namespace Nexta.Infrastructure.Minio
{
	public class MinioService : IMinioService
	{
		private readonly IMinioClient _minioClient;
		private readonly ILogger<MinioService> _logger;
		public MinioService(IMinioClient minioClient, ILogger<MinioService> logger)
		{
			_minioClient = minioClient;
			_logger = logger;
		}

		public async Task<List<Image>> GetFilesAsync(string bucket, CancellationToken ct = default, params string[] fileNames)
		{
			var images = new List<Image>();

			foreach (var name in fileNames)
			{
				try
				{
					using (var memoryStream = new MemoryStream())
					{
						var file = await _minioClient.GetObjectAsync(
							new GetObjectArgs()
								.WithBucket(bucket)
								.WithObject(name)
								.WithCallbackStream(async (stream, ct) =>
								{
									await stream.CopyToAsync(memoryStream, ct);
								}),
							ct);

						var base64string = Convert.ToBase64String(memoryStream.ToArray());

						images.Add(new Image
						{
							Name = name,
							Base64String = base64string
						});
					}
				}
				catch (ObjectNotFoundException ex)
				{
					_logger.LogWarning("Файл {name} не найден.", name);
				}
				catch (BucketNotFoundException ex)
				{
					_logger.LogWarning("Баскет {bucket} не найден.", bucket);
				}
			}
			return images;
		}
	}
}