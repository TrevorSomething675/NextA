using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Abstractions.Services;
using Nexta.Application.DTO;
using Nexta.Domain.Models;
using AutoMapper;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
	public class AddNewsCommandHandler : IRequestHandler<AddNewsCommandRequest, AddNewsCommandResponse>
	{
		private readonly IImageRepository _imageRepository;
		private readonly INewsRepository _newsRepository;
		private readonly IMinioService _minioService;
		private readonly IMapper _mapper;
		
		public AddNewsCommandHandler(INewsRepository newsRepository, IImageRepository imageRepository, 
			IMapper mapper, IMinioService minioService)
		{
			_imageRepository = imageRepository;
			_newsRepository = newsRepository;
			_minioService = minioService;
			_mapper = mapper;
		}

		public async Task<AddNewsCommandResponse> Handle(AddNewsCommandRequest request, CancellationToken ct = default)
		{
			var newsToCreate = _mapper.Map<News>(request);
			var news = await _newsRepository.AddAsync(newsToCreate);

			if (news.ImageId != null)
			{
				var createdDbImage = await _imageRepository.GetByIdAsync(news.ImageId.Value, ct);
				var createdMinioImage = await _minioService.AddFileAsync(request.Image.Base64String, news.Image.Name, "news", ct);
			}
			var result = _mapper.Map<NewsResponse>(news);

			return new AddNewsCommandResponse(result);
		}
	}
}