using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Queries.News.GetNewsQuery
{
	public class GetNewsQueryHandler : IRequestHandler<GetNewsQueryRequest, GetNewsQueryResponse>
	{
		private readonly INewsRepository _newsRepository;
		private readonly IMinioService _minioService;
		private readonly IMapper _mapper;

		public GetNewsQueryHandler(IMinioService minioService, INewsRepository newsRepository, IMapper mapper)
		{
			_newsRepository = newsRepository;
			_minioService = minioService;
			_mapper = mapper;
		}

		public async Task<GetNewsQueryResponse> Handle(GetNewsQueryRequest request, CancellationToken ct = default)
		{
			var news = await _newsRepository.GetAllAsync(ct);
			var newsImageIds = news.Select(n => n.Image!.Name).ToArray();
			Dictionary<string, ImageResponse>? imagesMap = null;

			if(newsImageIds.Length > 0)
			{
				var images = _mapper.Map<List<ImageResponse>>(await _minioService.GetFilesAsync("news", ct, newsImageIds));
				imagesMap = images.ToDictionary(img => img.Name);
			}

			var result = news.Select(n => new NewsResponse
			{
				Header = n.Header,
				Description = n.Description,
				Image = imagesMap?.GetValueOrDefault(n?.Image?.Name)
			}).ToList();

			return new GetNewsQueryResponse(result);
		}
	}
}