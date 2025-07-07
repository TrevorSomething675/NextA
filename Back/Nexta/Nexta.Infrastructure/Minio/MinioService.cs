using Nexta.Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;
using Minio.DataModel.Args;
using Nexta.Domain.Models;
using Minio.Exceptions;
using Minio;
using Nexta.Infrastructure.Exceptions;

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

		public async Task<Image> AddFileAsync(string base64String, string fileName, string bucket, CancellationToken ct = default)
		{
			try
			{
				var bytes = Convert.FromBase64String(base64String);

				using(var stream = new MemoryStream(bytes))
				{
					var result = await _minioClient.PutObjectAsync(
						new PutObjectArgs()
						.WithBucket(bucket)
						.WithObject(fileName)
						.WithStreamData(stream)
						.WithObjectSize(bytes.Length));

					return new Image
					{
						Bucket = bucket,
						Name = fileName,
					};
				}
			}
			catch(Exception ex)
			{
				throw new MinioFileCreationException(ex.Message);
			}
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
							Base64String = base64string,
							Bucket = bucket
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