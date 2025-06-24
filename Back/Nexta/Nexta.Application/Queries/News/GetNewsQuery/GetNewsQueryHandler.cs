using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
	public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryRequest, GetNewsQueryResponse>
	{
		private readonly IMinioService _minioService;
		private readonly IImageRepository _imageRepository;

		public GetNewsQueryHandler(IMinioService minioService, IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
			_minioService = minioService;
		}

		public async Task<GetNewsQueryResponse> Handle(GetNewsQueryRequest request, CancellationToken ct = default)
		{
			var dbImageNames = (await _imageRepository.GetAllAsync(true, ct))
				.Select(i => i.Name).ToArray();

			var images = await _minioService.GetFilesAsync("news", ct, dbImageNames);

			return new GetNewsQueryResponse(images);
		}
	}
}