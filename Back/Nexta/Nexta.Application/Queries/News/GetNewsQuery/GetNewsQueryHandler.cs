using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
	public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryRequest, GetNewsQueryResponse>
	{
		private readonly IImageRepository _imageRepository;
		private readonly IMinioService _minioService;
		private readonly IMapper _mapper;

		public GetNewsQueryHandler(IMinioService minioService, IImageRepository imageRepository, IMapper mapper)
		{
			_imageRepository = imageRepository;
			_minioService = minioService;
			_mapper = mapper;
		}

		public async Task<GetNewsQueryResponse> Handle(GetNewsQueryRequest request, CancellationToken ct = default)
		{
			var dbImageNames = (await _imageRepository.GetAllAsync(true, ct))
				.Select(i => i.Name).ToArray();

			var images = _mapper.Map<List<ImageResponse>>(await _minioService.GetFilesAsync("news", ct, dbImageNames));

			return new GetNewsQueryResponse(images);
		}
	}
}